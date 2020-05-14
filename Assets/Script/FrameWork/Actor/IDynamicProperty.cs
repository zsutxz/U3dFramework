using System;


namespace U3dFramework{

    public interface IDynamicProperty{
        void DoChangeProperty(int id, object oldValue, object newValue);

        PropertyItem GetProperty(int id);
    }

}