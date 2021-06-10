using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Constants
{
    public struct PageContentConstants
    {
        public static Guid Shipping = new Guid("00000000-0000-0000-0000-000000000001");
        public static Guid ContactUs = new Guid("00000000-0000-0000-0000-000000000002");
        public static Guid AboutUs = new Guid("00000000-0000-0000-0000-000000000003");

        private static string ContentShipping = "< h2 > Order with Prime FREE Same-Day Delivery</h2>" +
            "<p>Amazon Prime members shipping to select metro areas across the US can choose to receive FREE Same-Day Delivery on a broad selection of items.</p>" +
            "<p>&nbsp;</p><p>To qualify for Prime FREE Same-Day Delivery Prime members can:</p>" +
            "<ul><li>Place orders before the Same-Day Delivery order cutoff time" +
            ".The \"order within\" countdown timer provides the window of time in which you must place the order to receive your delivery by the date shown." +
            "That delivery date may become unavailable during that time due to changes in inventory or delivery capacity before you place your order." +
            "We'll include your confirmed delivery date in your order confirmation email.</li><li>Select Same-Day at checkout and shipping to a residential address within an " +
            "<a href=\"https://www.amazon.com/b/ref=psdd_help?node=8729023011\">eligible ZIP Code</a>.</li></ul><p>&nbsp;</p><p>&nbsp;</p><p>To find items and place orders with FREE Same-Day Delivery:</p>" +
            "<ol><li>Sign in to <a href=\"https://www.amazon.com/gp/css/homepage.html\">Your Account</a>.</li><li>Set or update your default shipping address in <a href=\"https://www.amazon.com/a/addresses\">Your Addresses</a>." +
            " This address must be residential and within an <a href=\"https://www.amazon.com/b/ref=psdd_help?node=8729023011\">eligible ZIP Code</a> for Prime FREE Same-Day delivery.</li>" +
            "<li>Add at least $35 of Same-Day-eligible products to your Shopping Cart. We'll show the shipping discount and delivery time in your<strong> Order Summary</strong>.</li>" +
            "</ol><p>&nbsp;</p><p>&nbsp;</p><p><strong>Note:</strong></p><ul><li>Same-Day Delivery is available seven days a week most days of the year, with limited availability on certain holidays and high-volume shopping days, including Black Friday and Prime Day." +
            "Same-Day delivery isn't available on Thanksgiving Day, Christmas Day, or New Year's Day.</li><li>Only residential addresses are eligible for Same-Day Delivery. Commercial addresses, P.O.Boxes, APO, FPO, and DPO addresses aren't eligible.</li>" +
            "<li>To order qualifying items over $35 via 1-Click on the product detail page, select <strong>Today Free</strong> and select <strong>Buy now with 1-Click</strong>.</li><li>You can pay for Same-Day Delivery if your order is under $35, or you're not a Prime member." +
            "For more information, go to <a href = \"://www.amazon.com/gp/help/customer/display.html?nodeId=GTFUTRQF459U3H3Y\" > Same - Day Delivery Rates</a>.</li><li>In some cases, if you choose a higher quantity for an item, " +
            "it will be ineligible for Same-Day Delivery because multiple units aren't available locally. In this case, we'll only show the normal Prime shipping options.</li></ul>";

        private static string ContentAboutUs= "<h2><strong>Our team is all over the world.</strong></h2>" +
            "<p>Educating and empowering the SEO community by providing the freshest news and latest best " +
            "practices via the industry’s smartest practitioners.</p><p>Launched in 2003, SEJ is unique in its " +
            "community-based approach to search marketing content. Virtually all of our contributed articles come " +
            "from real online marketing experts, both independent and in-house. SEJ is owned by " +
            "<a href=\"https://alphabrandmedia.com/\">Alpha Brand Media</a>.</p><p>Search Engine Journal is" +
            " an industry-leading publication that provides the latest news and insights about SEO, search, and " +
            "digital marketing.</p><p>Have a question for us, or feedback? Please click on the most appropriate " +
            "category below and fill out the form to reach us.</p><p><strong>PLEASE NOTE:</strong> We are not currently " +
            "accepting new contributors. But you are welcome to fill out the form. Trying to “sneak” in your application " +
            "via the wrong category will only result in deletion.</p>";

        public static readonly Dictionary<Guid, PageContent> ListPageContents = new Dictionary<Guid, PageContent>() {
            { Shipping, new PageContent(){  Order = 1,
                                            Title = "Shipping",
                                            Description = ContentShipping,
                                            ShortDes  = "Free Shipping World Wide",
                                            ImageUrl = "https://res.cloudinary.com/tungimage/image/upload/v1621094793/zqwbag7jhcifigugzcmy.png"} }, // Thay image
            { ContactUs, new PageContent(){ Order = 2,
                                            Title = "Contact Us",
                                            ShortDes = "Online Service For New Customer",
                                            ImageUrl = "https://res.cloudinary.com/tungimage/image/upload/v1621095055/ib5vgmob5h8s0je8wg6m.png"}} , // Thay image
            { AboutUs, new PageContent() {  Order = 3 ,
                                            Title = "About Us",
                                            Description = ContentAboutUs,
                                            ShortDes = "New Online Special Festival Offer",
                                            ImageUrl = "https://res.cloudinary.com/tungimage/image/upload/v1621095105/q0tohul7grgmkemfljza.png"}} // Thay image
        };
    }
}
