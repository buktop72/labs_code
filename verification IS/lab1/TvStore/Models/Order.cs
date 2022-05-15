public class Order
{
    public int OrderId { get; set; }
    public string User { get; set; } // имя фамилия покупателя
    public string Address { get; set; } // адрес покупателя
    public string ContactPhone { get; set; } // контактный телефон покупателя
    public string BonusCard { get; set; } // бонусная карта покупателя

    public int TvId { get; set; } // ссылка на связанную модель Phone
    public Tv Tv { get; set; }
}
