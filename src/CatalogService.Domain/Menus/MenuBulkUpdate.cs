namespace CatalogService.Menus;

public record MenuBulkUpdate(long Id, string Name, int SortOrder, long VendorId, bool IsDisabled);
