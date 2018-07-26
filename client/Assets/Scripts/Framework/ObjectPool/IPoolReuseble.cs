using System.Collections;
using System.Collections.Generic;

public interface IPoolReuseble {

    string ResName{ get; set; }

    void OnSpwan();

    void OnUnSpwan();
}
