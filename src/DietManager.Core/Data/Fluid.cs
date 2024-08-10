using System.Text.Json.Serialization;

namespace DietManager.Core.Data;

public class Fluid : IDietary
{
    /// <summary>
    ///     The amount of Kilojoules the fluid contains per 100 millilitres.
    /// </summary>
    public double? Kilojoules { get; init; }

    public double? Protein { get; init; }
    public double? Sodium { get; init; }
    public double? AddedSugars { get; init; }
    public double? TotalCarbs { get; init; }
    public double? SaturatedFat { get; init; }
    public double? TransUnsaturatedFat { get; init; }
    public double? UnsaturatedFat { get; init; }
    public double? MonoUnsaturatedFat { get; init; }
    public double? PolyUnsaturatedFat { get; init; }
    public double? OtherFat { get; init; }
    public double? TotalFat { get; init; }
    public double? VitaminA { get; init; }
    public double? VitaminB { get; init; }
    public double? VitaminC { get; init; }
    public double? VitaminD { get; init; }
    public double? VitaminK { get; init; }
    public double? Iron { get; init; }
    public double? Iodine { get; init; }
    public double? Zinc { get; init; }
    public double? Magnesium { get; init; }
    public double? Potassium { get; init; }
    public double? Omega3FattyAcids { get; init; }
    public double? FolicAcid { get; init; }
    public required Guid Id { get; init; }
    public string Name { get; init; } = null!;
    [JsonIgnore] public string JsonFile => "fluids.json";
}
