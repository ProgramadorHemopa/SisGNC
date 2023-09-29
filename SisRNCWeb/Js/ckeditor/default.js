/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.stylesSet.add('default', [
	{ name: 'Cabeçalho', element: 'h2', styles: { 'font-family': 'Times New Roman', 'font-size': '12pt', 'font-weight': 'bold', 'text-transform': 'uppercase', 'margin-bottom': '24pt' } },
	{ name: 'Dados do Processo', element: 'h2', styles: { 'font-family': 'Times New Roman', 'font-size': '12pt', 'font-style': 'italic', 'margin-bottom': '24pt' } },
	{ name: 'Título 1', element: 'h2', styles: { 'font-family': 'Times New Roman', 'font-size': '12pt', 'font-weight': 'bold', 'text-transform': 'uppercase', 'text-decoration': 'underline' } },
	{ name: 'Título 2', element: 'h2', styles: { 'font-family': 'Times New Roman', 'font-size': '12pt', 'font-weight': 'bold', 'text-transform': 'capitalize' } },
	{ name: 'Título 3', element: 'h2', styles: { 'font-family': 'Times New Roman', 'font-size': '12pt', 'font-weight': 'bold', 'font-style': 'italic' } },

    { name: 'Título Azul', element: 'h3', styles: { color: 'Blue' } },
    { name: 'Título Vermelho', element: 'h3', styles: { color: 'Red' } },
    { name: 'Marker: Yellow', element: 'span', styles: { 'background-color': 'Yellow' } },
    { name: 'Marker: Green', element: 'span', styles: { 'background-color': 'Lime' } },
    { name: 'Big', element: 'big' },
    { name: 'Small', element: 'small' },
    { name: 'Typewriter', element: 'tt' },
    { name: 'Computer Code', element: 'code' },
    { name: 'Keyboard Phrase', element: 'kbd' },
    { name: 'Sample Text', element: 'samp' },
    { name: 'Variable', element: 'var' },
    { name: 'Deleted Text', element: 'del' },
    { name: 'Inserted Text', element: 'ins' },
    { name: 'Cited Work', element: 'cite' },
    { name: 'Inline Quotation', element: 'q' },
    { name: 'Language: RTL', element: 'span', attributes: { dir: 'rtl' } },
    { name: 'Language: LTR', element: 'span', attributes: { dir: 'ltr' } },
    { name: 'Image on Left', element: 'img', attributes: { style: 'padding: 5px; margin-right: 5px', border: '2', align: 'left' } },
    { name: 'Image on Right', element: 'img', attributes: { style: 'padding: 5px; margin-left: 5px', border: '2', align: 'right' } },
    { name: 'Borderless Table', element: 'table', styles: { 'border-style': 'hidden', 'background-color': '#E6E6FA' } },
    { name: 'Square Bulleted List', element: 'ul', styles: { 'list-style-type': 'square' } }
]);
