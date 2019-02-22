# NUnit test retrying

This repository contains code presented on a skyrise.tech blog article:

[Extending NUnit 3 with command wrappers](https://www.skyrise.tech/blog/tech/extending-nunit-3-with-command-wrappers/)

It contains implementation and integration tests for a `Retrying` attribute, which retries failing tests. It was created, because NUnit's [Retry attribute](https://github.com/nunit/docs/wiki/Retry-Attribute) only retries tests on assertion failures, and not on other exceptions (as of 2017). This can be really inconvenient, especially with end-to-end tests.
