﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace PizzaServer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for PizzaServerRESTAPI.
    /// </summary>
    public static partial class PizzaServerRESTAPIExtensions
    {
            /// <summary>
            /// Returns all pizzas
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<Pizza> GetPizzasUsingGET(this IPizzaServerRESTAPI operations)
            {
                return Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).GetPizzasUsingGETAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns all pizzas
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<Pizza>> GetPizzasUsingGETAsync(this IPizzaServerRESTAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPizzasUsingGETWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Creates a new pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='pizza'>
            /// pizza
            /// </param>
            public static Pizza CreatePizzaUsingPOST(this IPizzaServerRESTAPI operations, Pizza pizza)
            {
                return Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).CreatePizzaUsingPOSTAsync(pizza), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Creates a new pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='pizza'>
            /// pizza
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Pizza> CreatePizzaUsingPOSTAsync(this IPizzaServerRESTAPI operations, Pizza pizza, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreatePizzaUsingPOSTWithHttpMessagesAsync(pizza, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Gets a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            public static Pizza GetPizzaUsingGET(this IPizzaServerRESTAPI operations, long id)
            {
                return Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).GetPizzaUsingGETAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Pizza> GetPizzaUsingGETAsync(this IPizzaServerRESTAPI operations, long id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPizzaUsingGETWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            public static void DeletePizzaUsingDELETE(this IPizzaServerRESTAPI operations, long id)
            {
                Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).DeletePizzaUsingDELETEAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeletePizzaUsingDELETEAsync(this IPizzaServerRESTAPI operations, long id, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeletePizzaUsingDELETEWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Retrieves the toppings of a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            public static IList<Topping> GetToppingsUsingGET(this IPizzaServerRESTAPI operations, long id)
            {
                return Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).GetToppingsUsingGETAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Retrieves the toppings of a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<Topping>> GetToppingsUsingGETAsync(this IPizzaServerRESTAPI operations, long id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetToppingsUsingGETWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Adds an existing topping to an existing pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            /// <param name='toppingId'>
            /// The topping ID
            /// </param>
            public static void AddToppingUsingPOST(this IPizzaServerRESTAPI operations, long id, long toppingId)
            {
                Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).AddToppingUsingPOSTAsync(id, toppingId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Adds an existing topping to an existing pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// The pizza ID
            /// </param>
            /// <param name='toppingId'>
            /// The topping ID
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task AddToppingUsingPOSTAsync(this IPizzaServerRESTAPI operations, long id, long toppingId, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.AddToppingUsingPOSTWithHttpMessagesAsync(id, toppingId, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Removes topping from a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='pizzaId'>
            /// The pizza ID
            /// </param>
            /// <param name='toppingId'>
            /// The topping ID
            /// </param>
            public static void DeleteToppingUsingDELETE(this IPizzaServerRESTAPI operations, long pizzaId, long toppingId)
            {
                Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).DeleteToppingUsingDELETEAsync(pizzaId, toppingId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Removes topping from a pizza
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='pizzaId'>
            /// The pizza ID
            /// </param>
            /// <param name='toppingId'>
            /// The topping ID
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteToppingUsingDELETEAsync(this IPizzaServerRESTAPI operations, long pizzaId, long toppingId, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeleteToppingUsingDELETEWithHttpMessagesAsync(pizzaId, toppingId, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Creates a new topping
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='topping'>
            /// topping
            /// </param>
            public static Topping CreateToppingUsingPOST(this IPizzaServerRESTAPI operations, Topping topping)
            {
                return Task.Factory.StartNew(s => ((IPizzaServerRESTAPI)s).CreateToppingUsingPOSTAsync(topping), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Creates a new topping
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='topping'>
            /// topping
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Topping> CreateToppingUsingPOSTAsync(this IPizzaServerRESTAPI operations, Topping topping, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateToppingUsingPOSTWithHttpMessagesAsync(topping, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
