'use strict';

$(() => {
    $('#DonationAmount').on('change', function() {
        const isCoveringCosts = $('#IsCoveringCosts').prop('checked');
        const amount = $(this).val();

        if (isNaN(amount) || Number(amount) <= 0.0) {
            $('#CoveredCosts').text('');
            $('#TotalDonationAmount').text('');
        }

        if (isCoveringCosts && amount > 0) {
            fetch(`/api/feecalculator?amount=${amount}`)
                .then(response => response.json())
                .then(data => {
                    $('#CoveredCosts').text('$' + data);
                    const total = data + Number(amount);
                    $('#TotalDonationAmount').text('$' + total);
                });
        } else {
            $('#TotalDonationAmount').text('$' + amount);
        }
    });
});