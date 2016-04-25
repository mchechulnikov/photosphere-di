namespace Photosphere.DependencyInjection.Lifetimes
{
    public enum Lifetime
    {
        AlwaysNew,
        PerRequest,
        PerContainer,
        PerScope // TODO
    }
}