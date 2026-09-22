using CatalogService.Localization;

using Volo.Abp;

namespace CatalogService.Files;

public class FileManager(IFileRepository fileRepository)
{
    public async Task<FileEntity> AddRelatedFileWithEntity(
    long entityId,
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
    long entityId,
    string entityType,
    string url,
    string fileName,
    long size,
    string extension,
    string role,
    IDictionary<string, object> variants)
    {
        var existing = await fileRepository.FindRelatedAsync(entityId, entityType, role);

        if (existing is null)
        {
            return await fileRepository.InsertAsync(new FileEntity
            {
                EntityId = entityId,
                EntityType = entityType,
                Url = url,
                FileName = fileName,
                Size = size,
                Extension = extension,
                Role = role,
                Variants = variants
            }, true);
        }

        existing.FileName = fileName;
        existing.Size = size;
        existing.Extension = extension;
        existing.Url = url;
        existing.Variants = variants;

        return await fileRepository.UpdateAsync(existing, true);
    }
    public async Task<FileEntity> SetPriorityFirst(long fileId)
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
    IDictionary<long, int> filePriority)
    {
        if (!filePriority.Any())
            return;

        var ids = filePriority.Keys.ToList();

        var files = await fileRepository.GetListAsync(x =>
            ids.Contains(x.Id));

        if (files.Count != ids.Count)
            throw new BusinessException(
                    CatalogServiceErrorCodes.SomeFilesNotFound,
                    CatalogServiceErrorCodes.SomeFilesNotFound)
                .WithResource(typeof(CatalogServiceResources));

        ValidateFilesBelongToSameScope(files);

        await SetPriorityInternal(filePriority);
    }
    private async Task SetPriorityInternal(
        IDictionary<long, int> filePriority)
    {
        if (!filePriority.Any())
            return;

        if (filePriority.Values.Any(x => x <= 0))
            throw new BusinessException(
                    CatalogServiceErrorCodes.PriorityMustBeGreaterThanZero,
                    CatalogServiceErrorCodes.PriorityMustBeGreaterThanZero)
                .WithResource(typeof(CatalogServiceResources));

        if (filePriority.Values.Distinct().Count() != filePriority.Count)
            throw new BusinessException(
                    CatalogServiceErrorCodes.DuplicatePrioritiesNotAllowed,
                    CatalogServiceErrorCodes.DuplicatePrioritiesNotAllowed)
                .WithResource(typeof(CatalogServiceResources));

        var ids = filePriority.Keys.ToList();

        var files = await fileRepository.GetListAsync(x =>
            ids.Contains(x.Id));

        if (files.Count != ids.Count)
            throw new BusinessException(
                    CatalogServiceErrorCodes.SomeFilesNotFound,
                    CatalogServiceErrorCodes.SomeFilesNotFound)
                .WithResource(typeof(CatalogServiceResources));

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
                    CatalogServiceErrorCodes.NoFilesFound,
                    CatalogServiceErrorCodes.NoFilesFound)
                .WithResource(typeof(CatalogServiceResources));

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
                    CatalogServiceErrorCodes.FilesMustBelongToSameScope,
                    CatalogServiceErrorCodes.FilesMustBelongToSameScope)
                .WithResource(typeof(CatalogServiceResources));
        }
    }
}
