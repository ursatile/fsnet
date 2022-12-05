---
title: Contents
layout: home
nav_order: 00
---
Welcome! This is the online handbook for Dylan Beattie's workshop "Full Stack Web Development with .NET 6".

Here's what's covered in each section of the workshop:

<ul id="index-nav">
    {% assign contents = site.pages | where_exp:"item", "item.summary != nil" %}
    {% for page in contents %}
    <li>
        <a href="{{ page.url | relative_url }}">{{ page.title }}</a>
        <p>{{ page.summary }}</p>
    </li>
    {% endfor %}
</ul>
