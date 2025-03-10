﻿using Api.Domain.Common;

namespace Api.Domain.Entities
{
    public class Category : EntityBase, IEntityBase
    {
        public Category()
        {
            
        }

        public Category(int parentId, string name, int priorty)
        {
            ParentId = parentId;
            Name = name;
            Priorty = priorty;

        }
        public  int ParentId { get; set; } //kesinlikle girmek zorundayım required yapıldı.

        public  string Name { get; set; }

        public  int Priorty { get; set; }

        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}

