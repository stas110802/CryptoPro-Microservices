using System.ComponentModel.DataAnnotations;
using CryptoPro.BotsService.Domain.Types;

namespace CryptoPro.BotsService.Domain.Dtos;

public sealed class SltpOrderCreateDto
{
    [Required]
    public string Currency { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public decimal SellPrice { get; set; }
    
    [Required]
    public decimal UpperPrice { get; set; }
    
    [Required]
    public decimal BottomPrice { get; set; }
    
    [Required]
    public SltpOrderType Type { get; set; }
    
    [Required]
    public ExchangeType ExchangeType { get; set; }
    
    [Required]
    public int UserId { get; set; }
}