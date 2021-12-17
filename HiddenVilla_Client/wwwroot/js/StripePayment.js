redirectToCheckout = function(sessionId) {
    var stripe = Stripe('pk_test_51K6RkWHvRkrqsxiDQgCTLmZqgD0lfRQ6qozj6JTmXfH7w26pof8jjSNdRpZkjUEHWWuJqNcH8xDGsw9etlYoaBTn001UehB7Bx');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};

