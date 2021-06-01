'use strict';

$(() => {
    $('#DonationAmount').on('change', function() {
        const isCoveringCosts = $('#IsCoveringCosts').prop('checked');
        const amount = $(this).val();

        tryUpdateTotals(amount, isCoveringCosts);
    });

    $('#IsCoveringCosts').on('change', function () {
        const isCoveringCosts = $(this).prop('checked');
        const amount = $('#DonationAmount').val();

        tryUpdateTotals(amount, isCoveringCosts);
    });

    function tryUpdateTotals(amount, isCoveringCosts) {
        if (isNaN(amount)) {
            $('#CoveredCosts').text('');
            $('#TotalDonationAmount').text('');
            return;
        }

        if (isCoveringCosts) {
            $('#CoveredCostsContainer').show();
            getTotalWithCoveredCost(amount);
        } else {
            $('#CoveredCosts').text('');
            $('#TotalDonationAmount').text('$' + currency(amount));
            $('#CoveredCostsContainer').hide();
        }
    }

    function getTotalWithCoveredCost(amount) {
        const value = currency(amount);

        if (value > 0) {
            fetch(`/api/feecalculator?amount=${value}`)
                .then(response => response.json())
                .then(data => {
                    $('#CoveredCosts').text('$' + currency(data));
                    const total = currency(data).add(value);
                    $('#TotalDonationAmount').text('$' + total);
                });
        } else {
            $('#CoveredCosts').text('');
            $('#TotalDonationAmount').text('$' + value);
        }
    }
});