// Thermal receipt printing function for 88mm thermal printers
function printThermalReceipt(orderData) {
    try {
        // Create a hidden iframe for printing
        const iframe = document.createElement('iframe');
        iframe.style.position = 'fixed';
        iframe.style.right = '0';
        iframe.style.bottom = '0';
        iframe.style.width = '88mm';
        iframe.style.height = '0';
        iframe.style.border = 'none';
        iframe.style.visibility = 'hidden';
        document.body.appendChild(iframe);

        const doc = iframe.contentDocument || iframe.contentWindow.document;

        // Format functions
        function formatDate(dateString) {
            const date = new Date(dateString);
            return date.toLocaleDateString('en-GB');
        }

        function formatCurrency(amount) {
            return new Intl.NumberFormat().format(amount) + ' MMK';
        }

        function truncateText(text, maxLength) {
            if (!text) return '';
            return text.length > maxLength ? text.substring(0, maxLength - 3) + '...' : text;
        }

        function getItemDisplayName(item) {
            if (item.categoryId !== 3 && item.categoryId !== 4)
                return item.categoryName || item.productItemName;
            return item.productItemName;
        }

        function getSizeDisplay(item) {
            let size = item.productSize.toString();
            if (item.categoryId === 1)
                size += '"';
            else if (item.categoryId === 2)
                size += ' ppl';
            return size;
        }

        // Build receipt HTML
        let receiptHTML = `
        <!DOCTYPE html>
        <html>
        <head>
            <title>Sales Slip</title>
            <meta charset="UTF-8">
            <style>
                @page {
                    margin: 0;
                    padding: 0;
                    size: 88mm auto;
                }
                body {
                    margin: 0;
                    padding: 3mm;
                    width: 88mm;
                    background: white;
                    font-family: 'Courier New', monospace;
                    font-size: 11px;
                    line-height: 1;
                }
                * {
                    margin: 0;
                    padding: 0;
                    box-sizing: border-box;
                }
                .receipt {
                    width: 100%;
                    max-width: 88mm;
                }
                .receipt-header {
                    text-align: center;
                    margin-bottom: 6px;
                    border-bottom: 1px dashed #000;
                    padding-bottom: 6px;
                }
                .receipt-header h2 {
                    font-size: 14px;
                    margin-bottom: 3px;
                    font-weight: bold;
                }
                .section {
                    margin-bottom: 8px;
                    padding-bottom: 6px;
                    border-bottom: 1px dashed #000;
                }
                .section-title {
                    font-weight: bold;
                    margin-bottom: 4px;
                    text-decoration: underline;
                }
                .details-grid div {
                    margin-bottom: 2px;
                }
                .order-items {
                    width: 100%;
                }
                .order-items table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-bottom: 8px;
                    table-layout: fixed;
                }
                .order-items th {
                    border-bottom: 1px dashed #000;
                    padding: 3px 1px;
                    text-align: left;
                    font-weight: bold;
                    font-size: 9px;
                }
                .order-items td {
                    padding: 2px 1px;
                    border-bottom: 1px dashed #eee;
                    font-size: 9px;
                    vertical-align: top;
                    word-wrap: break-word;
                }
                /* Fixed column widths for thermal printer */
                .col-item {
                    width: 35%;
                }
                .col-size {
                    width: 15%;
                }
                .col-qty {
                    width: 10%;
                    text-align: center;
                }
                .col-price {
                    width: 20%;
                    text-align: right;
                }
                .col-total {
                    width: 20%;
                    text-align: right;
                }
                .total-section {
                    margin-top: 8px;
                    padding-top: 6px;
                    border-top: 1px dashed #000;
                    text-align: center;
                    font-weight: bold;
                }
                .receipt-footer {
                    text-align: center;
                    margin-top: 12px;
                    padding-top: 6px;
                    border-top: 1px dashed #000;
                    font-style: italic;
                    font-size: 10px;
                }
                .item-details {
                    font-size: 8px;
                    color: #666;
                    padding-left: 5px;
                }
            </style>
        </head>
        <body>
            <div class="receipt">
                <div class="receipt-header">
                    <h2>${orderData.storeName ? orderData.storeName.toUpperCase() : 'Cake By Htoo'}</h2>
                    <div>Tel: 09-797910344,09-786555820</div>
                    <div>Order: ORD_${orderData.orderId.toString().padStart(8, '0')}</div>
                </div>

                <div class="section">
                    <div class="section-title">ORDER DETAILS</div>
                    <div class="details-grid">
                        <div><strong>Order Date:</strong> ${formatDate(orderData.orderDate)}</div>
                        <div><strong>Pickup Date:</strong> ${orderData.pickupDate ? formatDate(orderData.pickupDate) : 'ASAP'}</div>
                        <div><strong>Order Note:</strong> ${orderData.orderNote || '-'}</div>
                    </div>
                </div>

                <div class="section">
                    <div class="section-title">CUSTOMER</div>
                    <div class="details-grid">
                        <div><strong>Name:</strong> ${orderData.customerName}</div>
                        <div><strong>Phone:</strong> ${orderData.customerPhone}</div>
                        <div><strong>Address:</strong> ${orderData.customerAddress}</div>
                    </div>
                </div>

                <div class="section">
                    <div class="section-title">ORDER ITEMS</div>
                    <div class="order-items">
                        <table>
                            <thead>
                                <tr>
                                    <th class="col-item">ITEM</th>
                                    <th class="col-size">SIZE</th>
                                    <th class="col-qty">QTY</th>
                                    <th class="col-price">PRICE</th>
                                    <th class="col-total">SUB-TOTAL</th>
                                </tr>
                            </thead>
                            <tbody>`;

        // Add order items
        orderData.orderItems.forEach(item => {
            const itemName = getItemDisplayName(item);
            const size = getSizeDisplay(item);
            const unitPrice = item.unitPrice;
            const totalPrice = (item.unitPrice + (item.extraPrice || 0)) * item.quantity;

            receiptHTML += `
                                <tr>
                                    <td class="col-item">${truncateText(itemName, 20)}</td>
                                    <td class="col-size">${size}</td>
                                    <td class="col-qty">${item.quantity}</td>
                                    <td class="col-price">${formatCurrency(unitPrice)}</td>
                                    <td class="col-total">${formatCurrency(totalPrice)}</td>
                                </tr>`;

            // Add accessory if exists
            if (item.accessory) {
                receiptHTML += `
                                <tr>
                                    <td colspan="5" class="item-details">Accessory+ ${truncateText(item.accessory, 30)}</td>
                                </tr>`;
            }
            // Add extraPrice if exists
            if (item.extraPrice) {
                receiptHTML += `
                                <tr>
                                    <td colspan="5" class="item-details">ExtraPrice: ${formatCurrency(item.extraPrice || 0)}</td>
                                </tr>`;
            }
            // Add note if exists
            if (item.cakeNote) {
                receiptHTML += `
                                <tr>
                                    <td colspan="5" class="item-details">Note: ${truncateText(item.cakeNote, 40)}</td>
                                </tr>`;
            }

            // Add flavour if exists
            if (item.flavourName && item.flavourName.trim() !== '' && (item.categoryId == 1 || item.categoryId == 2)) {
                receiptHTML += `
                                <tr>
                                    <td colspan="5" class="item-details">Flavour: ${truncateText(item.flavourName, 35)}</td>
                                </tr>`;
            }
        });

        receiptHTML += `
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="total-section">
                    <div style="font-size: 12px;">TOTAL AMOUNT: <strong>${formatCurrency(orderData.totalAmount)}</strong></div>
                </div>

                <div class="receipt-footer">
                    *** THANK YOU ***<br>
                   Thank you for your purchase! 🍰<br>
                      Life is short, eat more cake.<br>
                    Contact: 09-797910344,09-786555820
                </div>

                <div style="text-align: center; margin-top: 8px; font-size: 8px;">
                    ${new Date().toLocaleString()}
                </div>
            </div>
        </body>
        </html>`;

        // Write to iframe
        doc.open();
        doc.write(receiptHTML);
        doc.close();

        // Flag to track if print has been called
        let printCalled = false;

        // Wait for iframe to load then print - but only once
        iframe.onload = function () {
            if (!printCalled) {
                printCalled = true;

                // Small delay to ensure content is fully rendered
                setTimeout(function () {
                    try {
                        iframe.contentWindow.focus();
                        iframe.contentWindow.print();
                    } catch (e) {
                        console.error('Print error:', e);
                    }

                    // Remove iframe after printing (regardless of print outcome)
                    setTimeout(function () {
                        if (document.body.contains(iframe)) {
                            document.body.removeChild(iframe);
                        }
                    }, 1000);
                }, 100);
            }
        };

        // Fallback: if onload doesn't fire, set a timeout to print anyway
        setTimeout(function () {
            if (!printCalled && document.body.contains(iframe)) {
                printCalled = true;
                try {
                    iframe.contentWindow.focus();
                    iframe.contentWindow.print();
                } catch (e) {
                    console.error('Print error:', e);
                }

                setTimeout(function () {
                    if (document.body.contains(iframe)) {
                        document.body.removeChild(iframe);
                    }
                }, 1000);
            }
        }, 2000);

    } catch (error) {
        console.error('Print error:', error);
        // Fallback to basic print
        alert('Printing failed. Please use browser print (Ctrl+P)');
    }
}

// Alternative simple print function without iframe
function simplePrintReceipt(orderData) {
    const printWindow = window.open('', '_blank', 'width=400,height=600');
    if (!printWindow) {
        alert('Popup blocked. Please allow popups for this site and try again.');
        return;
    }

    // Same formatting functions as above...
    function formatDate(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString('en-GB');
    }

    function formatCurrency(amount) {
        return new Intl.NumberFormat().format(amount) + ' MMK';
    }

    function truncateText(text, maxLength) {
        if (!text) return '';
        return text.length > maxLength ? text.substring(0, maxLength - 3) + '...' : text;
    }

    function getItemDisplayName(item) {
        if (item.categoryId !== 3 && item.categoryId !== 4)
            return item.categoryName || item.productItemName;
        return item.productItemName;
    }

    function getSizeDisplay(item) {
        let size = item.productSize.toString();
        if (item.categoryId === 1)
            size += '"';
        else if (item.categoryId === 2)
            size += ' ppl';
        return size;
    }

    // Build the same receipt HTML as above...
    let receiptHTML = `
    <!DOCTYPE html>
    <html>
    <head>
        <title>Sales Slip</title>
        <meta charset="UTF-8">
        <style>
            @page { margin: 0; padding: 0; size: 88mm auto; }
            body { margin: 0; padding: 3mm; width: 88mm; background: white; font-family: 'Courier New', monospace; font-size: 11px; line-height: 1; }
            /* Include all the same CSS styles as above */
            .receipt { width: 100%; max-width: 88mm; }
            .receipt-header { text-align: center; margin-bottom: 6px; border-bottom: 1px dashed #000; padding-bottom: 6px; }
            .receipt-header h2 { font-size: 14px; margin-bottom: 3px; font-weight: bold; }
            .section { margin-bottom: 8px; padding-bottom: 6px; border-bottom: 1px dashed #000; }
            .section-title { font-weight: bold; margin-bottom: 4px; text-decoration: underline; }
            .order-items table { width: 100%; border-collapse: collapse; margin-bottom: 8px; table-layout: fixed; }
            .order-items th { border-bottom: 1px dashed #000; padding: 3px 1px; text-align: left; font-weight: bold; font-size: 9px; }
            .order-items td { padding: 2px 1px; border-bottom: 1px dashed #eee; font-size: 9px; vertical-align: top; word-wrap: break-word; }
            .col-item { width: 35%; } .col-size { width: 15%; } .col-qty { width: 10%; text-align: center; }
            .col-price { width: 20%; text-align: right; } .col-total { width: 20%; text-align: right; }
            .total-section { margin-top: 8px; padding-top: 6px; border-top: 1px dashed #000; text-align: center; font-weight: bold; }
            .receipt-footer { text-align: center; margin-top: 12px; padding-top: 6px; border-top: 1px dashed #000; font-style: italic; font-size: 10px; }
            .item-details { font-size: 8px; color: #666; padding-left: 5px; }
        </style>
    </head>
    <body>
        <div class="receipt">
            <!-- Same receipt content as above -->
            <div class="receipt-header">
                <h2>${orderData.storeName ? orderData.storeName.toUpperCase() : 'Cake By Htoo'}</h2>
                <div>Tel: 09-797910344,09-786555820</div>
                <div>Order: ORD_${orderData.orderId.toString().padStart(8, '0')}</div>
            </div>

            <div class="section">
                <div class="section-title">ORDER DETAILS</div>
                <div class="details-grid">
                    <div><strong>Order Date:</strong> ${formatDate(orderData.orderDate)}</div>
                    <div><strong>Pickup Date:</strong> ${orderData.pickupDate ? formatDate(orderData.pickupDate) : 'ASAP'}</div>
                    <div><strong>Order Note:</strong> ${orderData.orderNote || '-'}</div>
                </div>
            </div>

            <div class="section">
                <div class="section-title">CUSTOMER</div>
                <div class="details-grid">
                    <div><strong>Name:</strong> ${orderData.customerName}</div>
                    <div><strong>Phone:</strong> ${orderData.customerPhone}</div>
                    <div><strong>Address:</strong> ${orderData.customerAddress}</div>
                </div>
            </div>

            <div class="section">
                <div class="section-title">ORDER ITEMS</div>
                <div class="order-items">
                    <table>
                        <thead>
                            <tr>
                                <th class="col-item">ITEM</th>
                                <th class="col-size">SIZE</th>
                                <th class="col-qty">QTY</th>
                                <th class="col-price">PRICE</th>
                                <th class="col-total">SUB-TOTAL</th>
                            </tr>
                        </thead>
                        <tbody>`;

    // Add order items
    orderData.orderItems.forEach(item => {
        const itemName = getItemDisplayName(item);
        const size = getSizeDisplay(item);
        const unitPrice = item.unitPrice;
        const totalPrice = (item.unitPrice + (item.extraPrice || 0)) * item.quantity;

        receiptHTML += `
                            <tr>
                                <td class="col-item">${truncateText(itemName, 20)}</td>
                                <td class="col-size">${size}</td>
                                <td class="col-qty">${item.quantity}</td>
                                <td class="col-price">${formatCurrency(unitPrice)}</td>
                                <td class="col-total">${formatCurrency(totalPrice)}</td>
                            </tr>`;

        if (item.accessory) {
            receiptHTML += `
                            <tr>
                                <td colspan="5" class="item-details">Accessory+ ${truncateText(item.accessory, 30)}</td>
                            </tr>`;
        }
        if (item.extraPrice) {
            receiptHTML += `
                            <tr>
                                <td colspan="5" class="item-details">ExtraPrice: ${formatCurrency(item.extraPrice || 0)}</td>
                            </tr>`;
        }
        if (item.cakeNote) {
            receiptHTML += `
                            <tr>
                                <td colspan="5" class="item-details">Note: ${truncateText(item.cakeNote, 40)}</td>
                            </tr>`;
        }
        if (item.flavourName && item.flavourName.trim() !== '' && (item.categoryId == 1 || item.categoryId == 2)) {
            receiptHTML += `
                            <tr>
                                <td colspan="5" class="item-details">Flavour: ${truncateText(item.flavourName, 35)}</td>
                            </tr>`;
        }
    });

    receiptHTML += `
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="total-section">
                <div style="font-size: 12px;">TOTAL AMOUNT: <strong>${formatCurrency(orderData.totalAmount)}</strong></div>
            </div>

            <div class="receipt-footer">
                *** THANK YOU ***<br>
                Thank you for your purchase! 🍰<br>
                Life is short, eat more cake.<br>
                Contact: 09-797910344,09-786555820
            </div>

            <div style="text-align: center; margin-top: 8px; font-size: 8px;">
                ${new Date().toLocaleString()}
            </div>
        </div>
        
        <script>
            // Auto print and close
            window.onload = function() {
                window.print();
                setTimeout(function() {
                    window.close();
                }, 500);
            };
            
            // If user cancels print, close after a while
            setTimeout(function() {
                window.close();
            }, 3000);
        </script>
    </body>
    </html>`;

    printWindow.document.write(receiptHTML);
    printWindow.document.close();
}