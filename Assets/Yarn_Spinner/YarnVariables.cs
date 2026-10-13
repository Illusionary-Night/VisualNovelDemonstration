using Yarn.Unity;

[System.CodeDom.Compiler.GeneratedCode("YarnSpinner", "3.2.4.0")]
public partial class YarnVariables : Yarn.Unity.InMemoryVariableStorage, Yarn.Unity.IGeneratedVariableStorage {
    // Accessor for Number $affection
    public float Affection {
        get => this.GetValueOrDefault<float>("$affection");
        set => this.SetValue<float>("$affection", value);
    }

    // Accessor for Bool $has_coffee
    public bool HasCoffee {
        get => this.GetValueOrDefault<bool>("$has_coffee");
        set => this.SetValue<bool>("$has_coffee", value);
    }

}
