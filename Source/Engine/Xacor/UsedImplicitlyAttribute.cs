using System;
using System.Diagnostics.CodeAnalysis;

namespace JetBrains.Annotations;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.All)]
internal sealed class UsedImplicitlyAttribute : Attribute
{
    public UsedImplicitlyAttribute() { }

    public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
    {
        UseKindFlags = useKindFlags;
    }

    public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
    {
        TargetFlags = targetFlags;
    }

    public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
    {
        UseKindFlags = useKindFlags;
        TargetFlags = targetFlags;
    }

    public ImplicitUseKindFlags UseKindFlags { get; }
    public ImplicitUseTargetFlags TargetFlags { get; }
}

[Flags]
internal enum ImplicitUseKindFlags
{
    Default = Access | Assign | InstantiatedWithFixedConstructorSignature,
    Access = 1,
    Assign = 2,
    InstantiatedWithFixedConstructorSignature = 4,
    InstantiatedNoFixedConstructorSignature = 8,
}

[Flags]
internal enum ImplicitUseTargetFlags
{
    Default = Itself,
    Itself = 1,
    Members = 2,
    WithMembers = Itself | Members
}