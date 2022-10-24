namespace StoreApi.Enums
{
	public enum ProductError
	{
		NameAlreadyExists,
		NameTooLong,
		NameTooShort,
		NameBeginsOrEndsWithWhitespace,
		QuantityOutOfRange,
		IncorrectCategory,
		StatusChangeBackToDraft,
		NewProductStatusNotDraft,
		OriginalProductNotFound,
		StatusChangeFromRemoved,
		PriceOutOfRange
	}
}
