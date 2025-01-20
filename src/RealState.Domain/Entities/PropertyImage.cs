using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealState.Domain.Common.Models;

namespace RealState.Domain.Entities
{
    public class PropertyImage : Entity
    {
        public string FileName { get; private set; }
        public string Description { get; private set; }
        public string File { get; private set; }
        public bool IsEnabled { get; private set; }
        public Guid PropertyId { get; private set; }
        public DateTime CreationDate { get; private set; }

        // Navigation property
        public virtual PropertyBuilding Property { get; private set; }

        // Private parameterless constructor for EF
        private PropertyImage() { }

        // Constructor for controlled creation with validation
        public PropertyImage(string fileName, string description, string file, bool isEnable, Guid propertyId)
        {
            if (string.IsNullOrWhiteSpace(fileName)) 
                throw new ArgumentException("File name cannot be empty.");
            if (string.IsNullOrWhiteSpace(description)) 
                throw new ArgumentException("File path cannot be empty.");

            FileName = fileName;
            Description = description;
            File = file;
            IsEnabled = isEnable;
            PropertyId = propertyId;
            CreationDate = DateTime.Now;
        }

        // Public static factory method to create instances with validation
        public static PropertyImage Create(string fileName, string description, string file, bool isEnable, Guid propertyId)
       {
           return new PropertyImage(fileName, description,file, isEnable, propertyId);
       }
    }
}