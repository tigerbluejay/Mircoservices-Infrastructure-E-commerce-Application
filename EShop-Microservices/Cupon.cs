namespace Discount.gRPC.Entities;

public class Cupon {
	public int Id { get; set; } // PK in Db
	public string ProductName { get; set; } // Identifier for Product associated with Cupon
	public string Description { get; set; }
	public decimal Amount { get; set; }
}