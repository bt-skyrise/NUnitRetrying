# NUnit test retrying

This repository contains code presented on a [Skyrise Tech Blog](http://blog.btskyrise.com/) (post not yet released).

It contains implementation and integration tests for a `Retrying` attribute, which retries failing tests. It was created, because NUnit's [Retry attribute](https://github.com/nunit/docs/wiki/Retry-Attribute) only retries tests on assertion failures, and not on other exceptions (which sometimes in inconvenient).