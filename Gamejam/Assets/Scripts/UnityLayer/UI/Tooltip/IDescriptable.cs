using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDescriptable
{
    bool IsShowable { get; }
    string Description { get; }
    string Title { get; }
}
