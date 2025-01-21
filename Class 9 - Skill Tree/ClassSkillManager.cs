using UnityEngine;
using System;
using System.Collections.Generic;

public class ClassSkillManager : MonoBehaviour
{
    [SerializeField] ClassSkill basicMelee;
    [SerializeField] ClassSkillTree skillTree; 

    public ClassSkillTree GetSkillTree()
    {
        return skillTree;
    }
}

[Serializable]
public class ClassSkillTree
{
    public List<ClassSkill> list;
}