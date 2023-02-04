using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilteredRewind : GenericRewind
{
    private bool m_Selected = false;
    public bool Selected
    {
        get { return m_Selected; }
        set { m_Selected = value; }
    }

    protected override void Rewind(float seconds)
    {
        if (m_Selected)
        {
            base.Rewind(seconds);
        }
    }
}
