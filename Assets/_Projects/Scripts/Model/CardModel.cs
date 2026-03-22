public class CardModel
{
    private CardView _cardView = new CardView();

    public int Value { get; set; }
    public CardModel ParentCard { get; set; }
    public CardModel ChildCard { get; set; }
    public bool IsOpen => ChildCard == null;

    public void OnModelChainged()
    {
        _cardView.Bind(this);
    }
}
