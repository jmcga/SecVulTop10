NewRelic NoticeError -

Can add additional error attributes via Key-Value pairs
	var errorAttributes = new Dictionary<string, string> { { "foo", "bar" }, { "baz", "luhr" } };

Examples of NoticeError Usage
	NewRelic.Api.Agent.NewRelic.NoticeError("String error message", errorAttributes);
	NewRelic.Api.Agent.NewRelic.NoticeError(ex, errorAttributes);
	NewRelic.Api.Agent.NewRelic.NoticeError("String error message", null);

