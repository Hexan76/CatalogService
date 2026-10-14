namespace CatalogService.Localization;

public static class CatalogServiceErrorCodes
{
    public const string CustomizationGroupNotFound = "400001";
    public const string VariantNotFound = "400002";
    public const string SomeFilesNotFound = "400003";
    public const string PriorityMustBeGreaterThanZero = "400004";
    public const string DuplicatePrioritiesNotAllowed = "400005";
    public const string NoFilesFound = "400006";
    public const string FilesMustBelongToSameScope = "400007";
    public const string CustomizationNotFound = "400008";
    public const string MenuNotFound = "400009";
    public const string ItemNotFound = "400010";
    public const string SortOrderCannotBeNegative = "400011";
}
