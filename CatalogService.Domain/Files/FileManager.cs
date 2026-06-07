using CatalogService.Files;

using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace CatalogService.Categories;

public class FileManager(IFileRepository fileRepository) : ITransientDependency
{
    public async Task<FileEntity> AddRelatedFileWithEntity(
        Guid? fileTempId,
        Guid entityId,
        string entityType,
        string url,
        string fileName,
        long size,
        string extension,
        string role,
        IDictionary<string, object> variants)
    {
        var item = new FileEntity()
        {
            FileName = fileName,
            EntityType = entityType,
            Size = size,
            Extension = extension,
            EntityId = entityId,
            Url = url,
            Role = role,
            Variants = variants
        };
        return await fileRepository.InsertAsync(item);
    }
    public async Task<FileEntity> AddOrUpdateRelatedFile(
    Guid entityId,
    string entityType,
    string url,
    string fileName,
    long size,
    string extension,
    string role,
    IDictionary<string, object> variants)
    {
        var existing = await fileRepository.FindAsync(x =>
            x.EntityId == entityId &&
            x.EntityType == entityType &&
            x.Role == role &&
            x.Url == url);

        if (existing == null)
        {
            var file = new FileEntity
            {
                EntityId = entityId,
                EntityType = entityType,
                Url = url,
                FileName = fileName,
                Size = size,
                Extension = extension,
                Role = role,
                Variants = variants
            };

            return await fileRepository.InsertAsync(file);
        }

        var changed =
            existing.FileName != fileName ||
            existing.Size != size ||
            existing.Extension != extension;

        if (!changed)
            return existing;

        existing.FileName = fileName;
        existing.Size = size;
        existing.Extension = extension;

        return await fileRepository.UpdateAsync(existing);
    }
    public async Task<FileEntity> SetPriorityFirst(Guid fileId)
    {
        var item = await fileRepository.GetAsync(fileId);

        var files = await fileRepository.GetListAsync(x =>
            x.EntityId == item.EntityId &&
            x.EntityType == item.EntityType &&
            x.Role == item.Role);

        if (item.Priority == 1)
            return item;

        foreach (var file in files)
        {
            if (file.Id == item.Id)
                continue;

            if (file.Priority < item.Priority)
                file.Priority++;
        }

        item.Priority = 1;

        await fileRepository.UpdateManyAsync(files);

        return await fileRepository.UpdateAsync(item);
    }

    public async Task SetPriority(
    IDictionary<Guid, int> filePriority)
    {
        if (!filePriority.Any())
            return;

        var ids = filePriority.Keys.ToList();

        var files = await fileRepository.GetListAsync(x =>
            ids.Contains(x.Id));

        if (files.Count != ids.Count)
            throw new BusinessException("Some files were not found.");

        ValidateFilesBelongToSameScope(files);

        await SetPriorityInternal(filePriority);
    }
    private async Task SetPriorityInternal(
        IDictionary<Guid, int> filePriority)
    {
        if (!filePriority.Any())
            return;

        if (filePriority.Values.Any(x => x <= 0))
            throw new BusinessException("Priority must be greater than zero.");

        if (filePriority.Values.Distinct().Count() != filePriority.Count)
            throw new BusinessException("Duplicate priorities are not allowed.");

        var ids = filePriority.Keys.ToList();

        var files = await fileRepository.GetListAsync(x =>
            ids.Contains(x.Id));

        if (files.Count != ids.Count)
            throw new BusinessException("Some files were not found.");

        foreach (var file in files)
        {
            file.Priority = filePriority[file.Id];
        }

        await fileRepository.UpdateManyAsync(files);
    }
    private static void ValidateFilesBelongToSameScope(
    IReadOnlyCollection<FileEntity> files)
    {
        if (!files.Any())
            throw new BusinessException(
                "No files found.");

        var scopeCount = files
            .Select(x => new
            {
                x.EntityId,
                x.EntityType,
                x.Role
            })
            .Distinct()
            .Count();

        if (scopeCount > 1)
        {
            throw new BusinessException(
                "All files must belong to the same scope.");
        }
    }
}
