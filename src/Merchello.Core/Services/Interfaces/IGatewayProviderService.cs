﻿using System;
using System.Collections.Generic;
using Merchello.Core.Models;
using Merchello.Core.Models.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace Merchello.Core.Services
{
    /// <summary>
    /// Defines the GatewayProviderService
    /// </summary>
    public interface IGatewayProviderService : IService
    {

        #region GatewayProvider

        /// <summary>
        /// Saves a single instance of a <see cref="IGatewayProvider"/>
        /// </summary>
        /// <param name="gatewayProvider"></param>
        /// <param name="raiseEvents">Optional boolean indicating whether or not to raise events</param>
        void Save(IGatewayProvider gatewayProvider, bool raiseEvents = true);

        /// <summary>
        /// Deletes a <see cref="IGatewayProvider"/>
        /// </summary>
        /// <param name="gatewayProvider"></param>
        /// <param name="raiseEvents">Optional boolean indicating whether or not to raise events</param>
        void Delete(IGatewayProvider gatewayProvider, bool raiseEvents = true);

        /// <summary>
        /// Gets a <see cref="IGatewayProvider"/> by it's unique 'Key' (Guid)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IGatewayProvider GetGatewayProviderByKey(Guid key);

        /// <summary>
        /// Gets a collection of <see cref="IGatewayProvider"/> by its type (Shipping, Taxation, Payment)
        /// </summary>
        /// <param name="gatewayProviderType"></param>
        /// <returns></returns>
        IEnumerable<IGatewayProvider> GetGatewayProvidersByType(GatewayProviderType gatewayProviderType);

        /// <summary>
        /// Gets a collection of <see cref="IGatewayProvider"/> by ship country
        /// </summary>
        /// <param name="shipCountry"></param>
        /// <returns></returns>
        IEnumerable<IGatewayProvider> GetGatewayProvidersByShipCountry(IShipCountry shipCountry); 

        /// <summary>
        /// Gets a collection containing all <see cref="IGatewayProvider"/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGatewayProvider> GetAllGatewayProviders(); 

        #endregion

        #region AppliedPayments

        /// <summary>
        /// Gets a collection of <see cref="IAppliedPayment"/>s by the payment key
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <returns>A collection of <see cref="IAppliedPayment"/></returns>
        IEnumerable<IAppliedPayment> GetAppliedPaymentsByPaymentKey(Guid paymentKey);

        /// <summary>
        /// Gets a collection of <see cref="IAppliedPayment"/>s by the invoice key
        /// </summary>
        /// <param name="invoiceKey">The invoice key</param>
        /// <returns>A collection of <see cref="IAppliedPayment"/></returns>
        IEnumerable<IAppliedPayment> GetAppliedPaymentsByInvoiceKey(Guid invoiceKey); 

        /// <summary>
        /// Saves a single <see cref="IAppliedPayment"/>
        /// </summary>
        /// <param name="appliedPayment">The <see cref="IAppliedPayment"/> to be saved</param>
        void Save(IAppliedPayment appliedPayment);

        #endregion

        #region Invoice

        /// <summary>
        /// Saves a single <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/> to save</param>
        void Save(IInvoice invoice);

        #endregion

        #region PaymentMethod

        /// <summary>
        /// Attempts to create a <see cref="IPaymentMethod"/> for a given provider.  If the provider already 
        /// defines a paymentCode, the creation fails.
        /// </summary>
        /// <param name="providerKey">The unique 'key' (Guid) of the TaxationGatewayProvider</param>
        /// <param name="name">The name of the payment method</param>
        /// <param name="description">The description of the payment method</param>
        /// <param name="paymentCode">The unique 'payment code' associated with the payment method.  (Eg. visa, mc)</param>
        /// <returns><see cref="Attempt"/> indicating whether or not the creation of the <see cref="IPaymentMethod"/> with respective success or fail</returns>
        Attempt<IPaymentMethod> CreatePaymentMethodWithKey(Guid providerKey, string name, string description, string paymentCode);

        /// <summary>
        /// Saves a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="paymentMethod">The <see cref="IPaymentMethod"/> to be saved</param>        
        void Save(IPaymentMethod paymentMethod);

        /// <summary>
        /// Deletes a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="paymentMethod">The <see cref="IPaymentMethod"/> to be deleted</param>        
        void Delete(IPaymentMethod paymentMethod);

        /// <summary>
        /// Gets a collection of <see cref="IPaymentMethod"/> for a given PaymentGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the PaymentGatewayProvider</param>
        /// <returns>A collection of <see cref="IPaymentMethod"/></returns>
        IEnumerable<IPaymentMethod> GetPaymentMethodsByProviderKey(Guid providerKey);

        #endregion

        #region Payment

        /// <summary>
        /// Creates a payment without saving it to the database
        /// </summary>
        /// <param name="paymentMethodType">The type of the paymentmethod</param>
        /// <param name="amount">The amount of the payment</param>
        /// <param name="paymentMethodKey">The optional paymentMethodKey</param>
        /// <returns>Returns <see cref="IPayment"/></returns>
        IPayment CreatePayment(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey);

        /// <summary>
        /// Creates and saves a payment
        /// </summary>
        /// <param name="paymentMethodType">The type of the paymentmethod</param>
        /// <param name="amount">The amount of the payment</param>
        /// <param name="paymentMethodKey">The optional paymentMethodKey</param>
        /// <returns>Returns <see cref="IPayment"/></returns>
        IPayment CreatePaymentWithKey(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey);

        /// <summary>
        /// Saves a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="payment">The <see cref="IPayment"/> to be saved</param>
        void Save(IPayment payment);

        /// <summary>
        /// Creates and saves an AppliedPayment
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <param name="invoiceKey">The invoice 'key'</param>
        /// <param name="appliedPaymentType">The applied payment type</param>
        /// <param name="description">The description of the payment application</param>
        /// <param name="amount">The amount of the payment to be applied</param>
        /// <returns>An <see cref="IAppliedPayment"/></returns>
        IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, decimal amount);

        /// <summary>
        /// Gets a collection of <see cref="IPayment"/> for a given invoice
        /// </summary>
        /// <param name="invoiceKey">The unique 'key' of the invoice</param>
        /// <returns>A collection of <see cref="IPayment"/></returns>
        IEnumerable<IPayment> GetPaymentsForInvoice(Guid invoiceKey);

        #endregion

        #region ShipMethod

        /// <summary>
        /// Creates a <see cref="IShipMethod"/>.  This is useful due to the data constraint
        /// preventing two ShipMethods being created with the same ShipCountry and ServiceCode for any provider.
        /// </summary>
        /// <param name="providerKey">The unique gateway provider key (Guid)</param>
        /// <param name="shipCountry">The <see cref="IShipCountry"/> this ship method is to be associated with</param>
        /// <param name="name">The required name of the <see cref="IShipMethod"/></param>
        /// <param name="serviceCode">The ShipMethods service code</param>
        Attempt<IShipMethod> CreateShipMethodWithKey(Guid providerKey, IShipCountry shipCountry, string name, string serviceCode);

        /// <summary>
        /// Saves a single <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethod"></param>
        void Save(IShipMethod shipMethod);

        /// <summary>
        /// Saves a collection of <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethodList">Collection of <see cref="IShipMethod"/></param>
        void Save(IEnumerable<IShipMethod> shipMethodList);

        /// <summary>
        /// Deletes a <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethod"></param>
        void Delete(IShipMethod shipMethod);

        /// <summary>
        /// Gets a list of <see cref="IShipMethod"/> objects given a <see cref="IGatewayProvider"/> key and a <see cref="IShipCountry"/> key
        /// </summary>
        /// <returns>A collection of <see cref="IShipMethod"/></returns>
        IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid providerKey, Guid shipCountryKey);

        /// <summary>
        /// Gets a list of all <see cref="IShipMethod"/> objects given a <see cref="IGatewayProvider"/> key
        /// </summary>
        /// <returns>A collection of <see cref="IShipMethod"/></returns>
        IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid providerKey); 

        #endregion

        #region ShipRateTier

        /// <summary>
        /// Saves a single <see cref="IShipRateTier"/>
        /// </summary>
        /// <param name="shipRateTier"></param>
        void Save(IShipRateTier shipRateTier);

        /// <summary>
        /// Saves a collection of <see cref="IShipRateTier"/>
        /// </summary>
        /// <param name="shipRateTierList"></param>
        void Save(IEnumerable<IShipRateTier> shipRateTierList);

        /// <summary>
        /// Deletes a <see cref="IShipRateTier"/>
        /// </summary>
        /// <param name="shipRateTier"></param>
        void Delete(IShipRateTier shipRateTier);

        /// <summary>
        /// Gets a list of <see cref="IShipRateTier"/> objects given a <see cref="IShipMethod"/> key
        /// </summary>
        /// <param name="shipMethodKey">Guid</param>
        /// <returns>A collection of <see cref="IShipRateTier"/></returns>
        IEnumerable<IShipRateTier> GetShipRateTiersByShipMethodKey(Guid shipMethodKey);

        #endregion

        #region ShipCountry

        /// <summary>
        /// Gets a <see cref="IShipCountry"/> by CatalogKey and CountryCode
        /// </summary>
        /// <param name="catalogKey">The unique key of the <see cref="IWarehouseCatalog"/></param>
        /// <param name="countryCode">The two character ISO country code</param>
        /// <returns>An <see cref="IShipCountry"/></returns>
        IShipCountry GetShipCountry(Guid catalogKey, string countryCode);

        /// <summary>
        /// Returns a collection of all <see cref="IShipCountry"/>
        /// </summary>
        /// <returns>A collection of all <see cref="IShipCountry"/></returns>
        IEnumerable<IShipCountry> GetAllShipCountries();

            #endregion

        #region Statuses


        /// <summary>
        /// Returns a collection of all <see cref="IInvoiceStatus"/>
        /// </summary>
        IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses();

        /// <summary>
        /// Returns a collection of all <see cref="IOrderStatus"/>
        /// </summary>
        IEnumerable<IOrderStatus> GetAllOrderStatuses();

        #endregion

        #region Taxmethod

        /// <summary>
        /// Attempts to create a <see cref="ITaxMethod"/> for a given provider and country.  If the provider already 
        /// defines a tax rate for the country, the creation fails.
        /// </summary>
        /// <param name="providerKey">The unique 'key' (Guid) of the TaxationGatewayProvider</param>
        /// <param name="countryCode">The two character ISO country code</param>
        /// <param name="percentageTaxRate">The tax rate in percentage for the country</param>
        /// <returns><see cref="Attempt"/> indicating whether or not the creation of the <see cref="ITaxMethod"/> with respective success or fail</returns>
        Attempt<ITaxMethod> CreateTaxMethodWithKey(Guid providerKey, string countryCode, decimal percentageTaxRate);

        /// <summary>
        /// Gets a <see cref="ITaxMethod"/> based on a provider and country code
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the <see cref="IGatewayProvider"/></param>
        /// <param name="countryCode">The country code of the <see cref="ITaxMethod"/></param>
        /// <returns>A collection <see cref="ITaxMethod"/></returns>
        ITaxMethod GetTaxMethodByCountryCode(Guid providerKey, string countryCode);

        /// <summary>
        /// Gets a collection of <see cref="ITaxMethod"/> based on a provider and country code
        /// </summary>
        /// <param name="countryCode">The country code of the <see cref="ITaxMethod"/></param>
        /// <returns><see cref="ITaxMethod"/></returns>
        IEnumerable<ITaxMethod> GetTaxMethodsByCountryCode(string countryCode);

        /// <summary>
        /// Saves a single <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="taxMethod">The <see cref="ITaxMethod"/> to be saved</param>        
        void Save(ITaxMethod taxMethod);

        /// <summary>
        /// Deletes a <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="taxMethod">The <see cref="ITaxMethod"/> to be deleted</param>
        void Delete(ITaxMethod taxMethod);

        /// <summary>
        /// Gets a collection of <see cref="ITaxMethod"/> for a given TaxationGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the TaxationGatewayProvider</param>
        /// <returns>A collection of <see cref="ITaxMethod"/></returns>
        IEnumerable<ITaxMethod> GetTaxMethodsByProviderKey(Guid providerKey);

        #endregion

    }
}