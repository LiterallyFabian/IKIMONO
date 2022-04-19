﻿using UnityEngine;

namespace ScriptableObjects
{
    public abstract class ItemScriptableObject : ScriptableObject
    {
        public string ItemName;
        public Sprite Sprite;
        public int PurchaseCost;
    }
}