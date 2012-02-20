namespace PlanetTelex.Web.Common.ModelViews
{
    /// <summary>
    /// The contract a model view must fulfill.
    /// </summary>
    interface IModelView
    {
        /// <summary>
        /// The HTML representation of the model.
        /// </summary>
        /// <returns></returns>
        string Html();
    }
}
