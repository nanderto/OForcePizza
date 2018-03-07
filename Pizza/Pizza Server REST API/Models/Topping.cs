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

    public partial class Topping
    {
        /// <summary>
        /// Initializes a new instance of the Topping class.
        /// </summary>
        public Topping() { }

        /// <summary>
        /// Initializes a new instance of the Topping class.
        /// </summary>
        public Topping(long id, string name = default(string))
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// The topping ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The name of the topping
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}