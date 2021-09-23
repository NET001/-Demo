using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternA
{
    public class Decorator : Component
    {
        Component component = null;

        public void SetComponent(Component component)
        {
            this.component = component;
        }

        public override void Show()
        {
            component.Show();
        }
    }
}
