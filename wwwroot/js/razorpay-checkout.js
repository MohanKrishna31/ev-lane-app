window.evLaneRazorpay = (() => {
    let loader;
    function load() {
        if (window.Razorpay) return Promise.resolve();
        if (loader) return loader;
        loader = new Promise((resolve, reject) => {
            const script = document.createElement("script");
            script.src = "https://checkout.razorpay.com/v1/checkout.js";
            script.onload = resolve;
            script.onerror = () => reject(new Error("Razorpay Checkout failed to load."));
            document.head.appendChild(script);
        });
        return loader;
    }
    async function open(checkout) {
        await load();
        return await new Promise((resolve, reject) => {
            const payment = new Razorpay({
                key: checkout.keyId,
                order_id: checkout.providerOrderId,
                amount: checkout.amountSubunits,
                currency: checkout.currency,
                name: "EVLane",
                description: checkout.description,
                prefill: { name: checkout.customerName, email: checkout.customerEmail, contact: checkout.customerMobile },
                handler: response => resolve(response),
                modal: { ondismiss: () => reject(new Error("Payment was cancelled.")) }
            });
            payment.on("payment.failed", response => reject(new Error(response.error?.description || "Payment failed.")));
            payment.open();
        });
    }
    return { open };
})();
