source 'https://rubygems.org'

gem "jekyll", "~> 4.3" # installed by `gem jekyll`
# gem "webrick"        # required when using Ruby >= 3 and Jekyll <= 4.2.2

gem "just-the-docs", "0.4.0.rc3" # currently the latest pre-release
# gem "just-the-docs"            # the latest release - currently 0.3.3

# Windows and JRuby does not include zoneinfo files, so bundle the tzinfo-data gem
# and associated library.
platforms :mingw, :x64_mingw, :mswin, :jruby do
    gem "tzinfo", "~> 1.2"
    gem "tzinfo-data"
end

gem "wdm", "~> 0.1.1", :platforms => [:mingw, :x64_mingw, :mswin]