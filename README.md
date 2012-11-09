LessDocs
========

They say less is more, although writing .less-css yields more css
than your original source. As if there weren't enough docs in this
world, this utility will let you document your (top-level only!)
.less rules and generate an HTML file that shows the examples
in source side-by-side with the actual rendered appearance. This way,
developers can share knowledge about reusable bits of css (hence,
top-level rules only) in a discoverable manner.

Example:
```css
/**
 * @name Defined with javadoc syntax
 * @category Navigation
 * @description Renders a menu as clickable tiles, identified
 *              by an image and a title
 * @example
 * <div class="navigation-tiles">
 *   <div class="tile><span class="title">Title</span></div>
 * </div>
 */
.navigation-tiles
{
	.tile { float: left; }
	.title { font-weight: bold; }
	// ...
}

// @name Defined with slash-syntax
// @category Navigation
// @description Turn a list into a tabbed header
// @example
// <div class="navigation-tabs">
//   <div class="tile><span class="title">Title</span></div>
//   <div class="content">Tab content goes here...</div>
// </div>
 .navigation-tabs
 {
     // ...
 }
```