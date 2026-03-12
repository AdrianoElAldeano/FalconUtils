using System.ComponentModel;

namespace FalconUtils;

public class Config
{
    [Description("Probabilidad de tarjeta Sup_InvHandler.inv en cientificos")]
    public int SuccessChance { get; set; } = 20;
}