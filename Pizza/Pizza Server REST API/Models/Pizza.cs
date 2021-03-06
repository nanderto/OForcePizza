﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace PizzaServer.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Pizza
    {
        /// <summary>
        /// Initializes a new instance of the Pizza class.
        /// </summary>
        public Pizza() { }

        /// <summary>
        /// Initializes a new instance of the Pizza class.
        /// </summary>
        public Pizza(long id, string name = default(string), string description = default(string))
        {
            Id = id;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// The pizza ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The name of the pizza
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The description of the pizza
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}
