using System.Collections.Generic;

public delegate void VoidVoidDelegate();
delegate int IntVoidDelegate();
delegate int IntIntDelegate(int _value);
delegate void VoidEnemyListDelegate(List<Enemy> _list);

public delegate void AttackDelegate(int _dmg);
public delegate void MissileStateDelegate(int _idx, bool _isFill);