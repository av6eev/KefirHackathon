using Specifications;
using UnityEngine;

namespace Entities.Enemy.Specification
{
    [CreateAssetMenu(menuName = "Create Specification Collection/New Enemy Collection", fileName = "EnemyCollectionSpecification", order = 51)]
    public class EnemyCollectionSpecificationScrObj : SpecificationCollectionScrObj<EnemySpecification>
    {
    }
}