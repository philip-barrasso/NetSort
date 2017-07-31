namespace NetSort.UnitTests.EF
{
    public class EF_Person
    {
        [Sortable("age")]
        public int Age { get; set; }

        [Sortable("score")]
        public EF_Score Score { get; set; }
    }
}
