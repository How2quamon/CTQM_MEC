const fullpageEl = document.getElementById("fullpage");
const menuBtn = document.querySelector(".menu__btn");
const navigation = document.querySelector(".navigation");
const navCloseBtn = document.querySelector(".navigation__close__btn");
const blurOverlay = document.querySelector(".blur__overlay");

const toggleNavigation = () => {
	navigation.classList.toggle("is--active");
	blurOverlay.classList.toggle("is--active");
	fullpageEl.classList.toggle("no-scroll");
};

if (menuBtn) menuBtn.addEventListener("click", toggleNavigation);
if (navCloseBtn) navCloseBtn.addEventListener("click", toggleNavigation);
if (blurOverlay) blurOverlay.addEventListener("click", toggleNavigation);
/*if (fullpageEl) fullpageEl = new fullpage("#fullpage", {
		autoScrolling: true,
		scrollBar: true
	});
	*/
