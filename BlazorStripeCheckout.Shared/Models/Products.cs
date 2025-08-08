using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BlazorStripeCheckout.Shared.Models;

[DataContract]
public class Products
{
    [Key]
    [DataMember]
    public long? Id { get; set; }

    [DataMember]
    public long? ProductCategoryId { get; set; }

    [DataMember]
    public string? Name { get; set; }

    [DataMember]
    public string? Description { get; set; }

    [DataMember]
    public decimal? Price { get; set; }

    [DataMember]
    public long? StockQuantity { get; set; }

    [DataMember]
    public string? Photo { get; set; }

    [DataMember]
    public string? Notes { get; set; }

    [DataMember]
    public string? Model { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public DateTime? ModifiedDate { get; set; }
}
