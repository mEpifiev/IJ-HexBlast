using Game.Scripts.Controls;
using Game.Scripts.General;
using Game.Scripts.Interact;
using Game.Scripts.Interfaces;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<HexGrid>().As<IHexGrid>();
            builder.RegisterComponentInHierarchy<InputReader>().As<IInputReader>();
            
            builder.Register<HexPlacementFinder>(Lifetime.Scoped);
            
            builder.RegisterComponentInHierarchy<HexBlockDragHandler>();
        }
    }
}