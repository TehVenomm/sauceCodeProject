package com.fasterxml.jackson.core.filter;

import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonParser.NumberType;
import com.fasterxml.jackson.core.JsonStreamContext;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.util.JsonParserDelegate;
import java.io.IOException;
import java.io.OutputStream;
import java.math.BigDecimal;
import java.math.BigInteger;

public class FilteringParserDelegate extends JsonParserDelegate {
    protected boolean _allowMultipleMatches;
    protected JsonToken _currToken;
    protected TokenFilterContext _exposedContext;
    protected TokenFilterContext _headContext;
    @Deprecated
    protected boolean _includeImmediateParent;
    protected boolean _includePath;
    protected TokenFilter _itemFilter;
    protected JsonToken _lastClearedToken;
    protected int _matchCount;
    protected TokenFilter rootFilter;

    public FilteringParserDelegate(JsonParser jsonParser, TokenFilter tokenFilter, boolean z, boolean z2) {
        super(jsonParser);
        this.rootFilter = tokenFilter;
        this._itemFilter = tokenFilter;
        this._headContext = TokenFilterContext.createRootContext(tokenFilter);
        this._includePath = z;
        this._allowMultipleMatches = z2;
    }

    public TokenFilter getFilter() {
        return this.rootFilter;
    }

    public int getMatchCount() {
        return this._matchCount;
    }

    public JsonToken getCurrentToken() {
        return this._currToken;
    }

    public final int getCurrentTokenId() {
        JsonToken jsonToken = this._currToken;
        if (jsonToken == null) {
            return 0;
        }
        return jsonToken.mo9113id();
    }

    public boolean hasCurrentToken() {
        return this._currToken != null;
    }

    public boolean hasTokenId(int i) {
        JsonToken jsonToken = this._currToken;
        if (jsonToken == null) {
            if (i == 0) {
                return true;
            }
            return false;
        } else if (jsonToken.mo9113id() != i) {
            return false;
        } else {
            return true;
        }
    }

    public final boolean hasToken(JsonToken jsonToken) {
        return this._currToken == jsonToken;
    }

    public boolean isExpectedStartArrayToken() {
        return this._currToken == JsonToken.START_ARRAY;
    }

    public boolean isExpectedStartObjectToken() {
        return this._currToken == JsonToken.START_OBJECT;
    }

    public JsonLocation getCurrentLocation() {
        return this.delegate.getCurrentLocation();
    }

    public JsonStreamContext getParsingContext() {
        return _filterContext();
    }

    public String getCurrentName() throws IOException {
        JsonStreamContext _filterContext = _filterContext();
        if (this._currToken != JsonToken.START_OBJECT && this._currToken != JsonToken.START_ARRAY) {
            return _filterContext.getCurrentName();
        }
        JsonStreamContext parent = _filterContext.getParent();
        if (parent == null) {
            return null;
        }
        return parent.getCurrentName();
    }

    public void clearCurrentToken() {
        if (this._currToken != null) {
            this._lastClearedToken = this._currToken;
            this._currToken = null;
        }
    }

    public JsonToken getLastClearedToken() {
        return this._lastClearedToken;
    }

    public void overrideCurrentName(String str) {
        throw new UnsupportedOperationException("Can not currently override name during filtering read");
    }

    public JsonToken nextToken() throws IOException {
        JsonToken jsonToken;
        if (!this._allowMultipleMatches && this._currToken != null && this._exposedContext == null) {
            if (this._currToken.isStructEnd() && this._headContext.isStartHandled()) {
                this._currToken = null;
                return null;
            } else if (this._currToken.isScalarValue() && !this._headContext.isStartHandled() && !this._includePath && this._itemFilter == TokenFilter.INCLUDE_ALL) {
                this._currToken = null;
                return null;
            }
        }
        TokenFilterContext tokenFilterContext = this._exposedContext;
        if (tokenFilterContext != null) {
            do {
                JsonToken nextTokenToRead = tokenFilterContext.nextTokenToRead();
                if (nextTokenToRead != null) {
                    this._currToken = nextTokenToRead;
                    return nextTokenToRead;
                } else if (tokenFilterContext == this._headContext) {
                    this._exposedContext = null;
                    if (tokenFilterContext.inArray()) {
                        JsonToken currentToken = this.delegate.getCurrentToken();
                        this._currToken = currentToken;
                        return currentToken;
                    }
                } else {
                    tokenFilterContext = this._headContext.findChildOf(tokenFilterContext);
                    this._exposedContext = tokenFilterContext;
                }
            } while (tokenFilterContext != null);
            throw _constructError("Unexpected problem: chain of filtered context broken");
        }
        JsonToken nextToken = this.delegate.nextToken();
        if (nextToken == null) {
            this._currToken = nextToken;
            return nextToken;
        }
        switch (nextToken.mo9113id()) {
            case 1:
                TokenFilter tokenFilter = this._itemFilter;
                if (tokenFilter == TokenFilter.INCLUDE_ALL) {
                    this._headContext = this._headContext.createChildObjectContext(tokenFilter, true);
                    this._currToken = nextToken;
                    return nextToken;
                } else if (tokenFilter == null) {
                    this.delegate.skipChildren();
                    break;
                } else {
                    TokenFilter checkValue = this._headContext.checkValue(tokenFilter);
                    if (checkValue == null) {
                        this.delegate.skipChildren();
                        break;
                    } else {
                        if (checkValue != TokenFilter.INCLUDE_ALL) {
                            checkValue = checkValue.filterStartObject();
                        }
                        this._itemFilter = checkValue;
                        if (checkValue == TokenFilter.INCLUDE_ALL) {
                            this._headContext = this._headContext.createChildObjectContext(checkValue, true);
                            this._currToken = nextToken;
                            return nextToken;
                        }
                        this._headContext = this._headContext.createChildObjectContext(checkValue, false);
                        if (this._includePath) {
                            JsonToken _nextTokenWithBuffering = _nextTokenWithBuffering(this._headContext);
                            if (_nextTokenWithBuffering != null) {
                                this._currToken = _nextTokenWithBuffering;
                                return _nextTokenWithBuffering;
                            }
                        }
                    }
                }
                break;
            case 2:
            case 4:
                boolean isStartHandled = this._headContext.isStartHandled();
                TokenFilter filter = this._headContext.getFilter();
                if (!(filter == null || filter == TokenFilter.INCLUDE_ALL)) {
                    filter.filterFinishArray();
                }
                this._headContext = this._headContext.getParent();
                this._itemFilter = this._headContext.getFilter();
                if (isStartHandled) {
                    this._currToken = nextToken;
                    return nextToken;
                }
                break;
            case 3:
                TokenFilter tokenFilter2 = this._itemFilter;
                if (tokenFilter2 == TokenFilter.INCLUDE_ALL) {
                    this._headContext = this._headContext.createChildArrayContext(tokenFilter2, true);
                    this._currToken = nextToken;
                    return nextToken;
                } else if (tokenFilter2 == null) {
                    this.delegate.skipChildren();
                    break;
                } else {
                    TokenFilter checkValue2 = this._headContext.checkValue(tokenFilter2);
                    if (checkValue2 == null) {
                        this.delegate.skipChildren();
                        break;
                    } else {
                        if (checkValue2 != TokenFilter.INCLUDE_ALL) {
                            checkValue2 = checkValue2.filterStartArray();
                        }
                        this._itemFilter = checkValue2;
                        if (checkValue2 == TokenFilter.INCLUDE_ALL) {
                            this._headContext = this._headContext.createChildArrayContext(checkValue2, true);
                            this._currToken = nextToken;
                            return nextToken;
                        }
                        this._headContext = this._headContext.createChildArrayContext(checkValue2, false);
                        if (this._includePath) {
                            JsonToken _nextTokenWithBuffering2 = _nextTokenWithBuffering(this._headContext);
                            if (_nextTokenWithBuffering2 != null) {
                                this._currToken = _nextTokenWithBuffering2;
                                return _nextTokenWithBuffering2;
                            }
                        }
                    }
                }
                break;
            case 5:
                String currentName = this.delegate.getCurrentName();
                TokenFilter fieldName = this._headContext.setFieldName(currentName);
                if (fieldName == TokenFilter.INCLUDE_ALL) {
                    this._itemFilter = fieldName;
                    if (this._includePath || !this._includeImmediateParent || this._headContext.isStartHandled()) {
                        jsonToken = nextToken;
                    } else {
                        jsonToken = this._headContext.nextTokenToRead();
                        this._exposedContext = this._headContext;
                    }
                    this._currToken = jsonToken;
                    return jsonToken;
                } else if (fieldName == null) {
                    this.delegate.nextToken();
                    this.delegate.skipChildren();
                    break;
                } else {
                    TokenFilter includeProperty = fieldName.includeProperty(currentName);
                    if (includeProperty == null) {
                        this.delegate.nextToken();
                        this.delegate.skipChildren();
                        break;
                    } else {
                        this._itemFilter = includeProperty;
                        if (includeProperty == TokenFilter.INCLUDE_ALL && this._includePath) {
                            this._currToken = nextToken;
                            return nextToken;
                        } else if (this._includePath) {
                            JsonToken _nextTokenWithBuffering3 = _nextTokenWithBuffering(this._headContext);
                            if (_nextTokenWithBuffering3 != null) {
                                this._currToken = _nextTokenWithBuffering3;
                                return _nextTokenWithBuffering3;
                            }
                        }
                    }
                }
                break;
            default:
                TokenFilter tokenFilter3 = this._itemFilter;
                if (tokenFilter3 == TokenFilter.INCLUDE_ALL) {
                    this._currToken = nextToken;
                    return nextToken;
                } else if (tokenFilter3 != null) {
                    TokenFilter checkValue3 = this._headContext.checkValue(tokenFilter3);
                    if (checkValue3 == TokenFilter.INCLUDE_ALL || (checkValue3 != null && checkValue3.includeValue(this.delegate))) {
                        this._currToken = nextToken;
                        return nextToken;
                    }
                }
                break;
        }
        return _nextToken2();
    }

    /* access modifiers changed from: protected */
    public final JsonToken _nextToken2() throws IOException {
        JsonToken nextToken;
        while (true) {
            nextToken = this.delegate.nextToken();
            if (nextToken == null) {
                this._currToken = nextToken;
                return nextToken;
            }
            switch (nextToken.mo9113id()) {
                case 1:
                    TokenFilter tokenFilter = this._itemFilter;
                    if (tokenFilter == TokenFilter.INCLUDE_ALL) {
                        this._headContext = this._headContext.createChildObjectContext(tokenFilter, true);
                        this._currToken = nextToken;
                        return nextToken;
                    } else if (tokenFilter == null) {
                        this.delegate.skipChildren();
                        break;
                    } else {
                        TokenFilter checkValue = this._headContext.checkValue(tokenFilter);
                        if (checkValue == null) {
                            this.delegate.skipChildren();
                            break;
                        } else {
                            if (checkValue != TokenFilter.INCLUDE_ALL) {
                                checkValue = checkValue.filterStartObject();
                            }
                            this._itemFilter = checkValue;
                            if (checkValue == TokenFilter.INCLUDE_ALL) {
                                this._headContext = this._headContext.createChildObjectContext(checkValue, true);
                                this._currToken = nextToken;
                                return nextToken;
                            }
                            this._headContext = this._headContext.createChildObjectContext(checkValue, false);
                            if (this._includePath) {
                                JsonToken _nextTokenWithBuffering = _nextTokenWithBuffering(this._headContext);
                                if (_nextTokenWithBuffering == null) {
                                    break;
                                } else {
                                    this._currToken = _nextTokenWithBuffering;
                                    return _nextTokenWithBuffering;
                                }
                            } else {
                                continue;
                            }
                        }
                    }
                case 2:
                case 4:
                    boolean isStartHandled = this._headContext.isStartHandled();
                    TokenFilter filter = this._headContext.getFilter();
                    if (!(filter == null || filter == TokenFilter.INCLUDE_ALL)) {
                        filter.filterFinishArray();
                    }
                    this._headContext = this._headContext.getParent();
                    this._itemFilter = this._headContext.getFilter();
                    if (!isStartHandled) {
                        break;
                    } else {
                        this._currToken = nextToken;
                        return nextToken;
                    }
                case 3:
                    TokenFilter tokenFilter2 = this._itemFilter;
                    if (tokenFilter2 == TokenFilter.INCLUDE_ALL) {
                        this._headContext = this._headContext.createChildArrayContext(tokenFilter2, true);
                        this._currToken = nextToken;
                        return nextToken;
                    } else if (tokenFilter2 == null) {
                        this.delegate.skipChildren();
                        break;
                    } else {
                        TokenFilter checkValue2 = this._headContext.checkValue(tokenFilter2);
                        if (checkValue2 == null) {
                            this.delegate.skipChildren();
                            break;
                        } else {
                            if (checkValue2 != TokenFilter.INCLUDE_ALL) {
                                checkValue2 = checkValue2.filterStartArray();
                            }
                            this._itemFilter = checkValue2;
                            if (checkValue2 == TokenFilter.INCLUDE_ALL) {
                                this._headContext = this._headContext.createChildArrayContext(checkValue2, true);
                                this._currToken = nextToken;
                                return nextToken;
                            }
                            this._headContext = this._headContext.createChildArrayContext(checkValue2, false);
                            if (this._includePath) {
                                JsonToken _nextTokenWithBuffering2 = _nextTokenWithBuffering(this._headContext);
                                if (_nextTokenWithBuffering2 == null) {
                                    break;
                                } else {
                                    this._currToken = _nextTokenWithBuffering2;
                                    return _nextTokenWithBuffering2;
                                }
                            } else {
                                continue;
                            }
                        }
                    }
                case 5:
                    String currentName = this.delegate.getCurrentName();
                    TokenFilter fieldName = this._headContext.setFieldName(currentName);
                    if (fieldName == TokenFilter.INCLUDE_ALL) {
                        this._itemFilter = fieldName;
                        this._currToken = nextToken;
                        return nextToken;
                    } else if (fieldName == null) {
                        this.delegate.nextToken();
                        this.delegate.skipChildren();
                        break;
                    } else {
                        TokenFilter includeProperty = fieldName.includeProperty(currentName);
                        if (includeProperty == null) {
                            this.delegate.nextToken();
                            this.delegate.skipChildren();
                            break;
                        } else {
                            this._itemFilter = includeProperty;
                            if (includeProperty == TokenFilter.INCLUDE_ALL) {
                                if (!this._includePath) {
                                    break;
                                } else {
                                    this._currToken = nextToken;
                                    return nextToken;
                                }
                            } else if (this._includePath) {
                                JsonToken _nextTokenWithBuffering3 = _nextTokenWithBuffering(this._headContext);
                                if (_nextTokenWithBuffering3 == null) {
                                    break;
                                } else {
                                    this._currToken = _nextTokenWithBuffering3;
                                    return _nextTokenWithBuffering3;
                                }
                            } else {
                                continue;
                            }
                        }
                    }
                default:
                    TokenFilter tokenFilter3 = this._itemFilter;
                    if (tokenFilter3 == TokenFilter.INCLUDE_ALL) {
                        this._currToken = nextToken;
                        return nextToken;
                    } else if (tokenFilter3 != null) {
                        TokenFilter checkValue3 = this._headContext.checkValue(tokenFilter3);
                        if (checkValue3 == TokenFilter.INCLUDE_ALL || (checkValue3 != null && checkValue3.includeValue(this.delegate))) {
                            this._currToken = nextToken;
                            break;
                        }
                    } else {
                        continue;
                    }
            }
        }
        this._currToken = nextToken;
        return nextToken;
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Code restructure failed: missing block: B:107:?, code lost:
        return _nextBuffered(r7);
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.fasterxml.jackson.core.JsonToken _nextTokenWithBuffering(com.fasterxml.jackson.core.filter.TokenFilterContext r7) throws java.io.IOException {
        /*
            r6 = this;
            r2 = 0
            r1 = 1
        L_0x0002:
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            com.fasterxml.jackson.core.JsonToken r3 = r0.nextToken()
            if (r3 != 0) goto L_0x000c
            r0 = r3
        L_0x000b:
            return r0
        L_0x000c:
            int r0 = r3.mo9113id()
            switch(r0) {
                case 1: goto L_0x0052;
                case 2: goto L_0x009e;
                case 3: goto L_0x001e;
                case 4: goto L_0x009e;
                case 5: goto L_0x00df;
                default: goto L_0x0013;
            }
        L_0x0013:
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r6._itemFilter
            com.fasterxml.jackson.core.filter.TokenFilter r3 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 != r3) goto L_0x0123
            com.fasterxml.jackson.core.JsonToken r0 = r6._nextBuffered(r7)
            goto L_0x000b
        L_0x001e:
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilter r3 = r6._itemFilter
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r0.checkValue(r3)
            if (r0 != 0) goto L_0x002e
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            r0.skipChildren()
            goto L_0x0002
        L_0x002e:
            com.fasterxml.jackson.core.filter.TokenFilter r3 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 == r3) goto L_0x0036
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r0.filterStartArray()
        L_0x0036:
            r6._itemFilter = r0
            com.fasterxml.jackson.core.filter.TokenFilter r3 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 != r3) goto L_0x0049
            com.fasterxml.jackson.core.filter.TokenFilterContext r2 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r2.createChildArrayContext(r0, r1)
            r6._headContext = r0
            com.fasterxml.jackson.core.JsonToken r0 = r6._nextBuffered(r7)
            goto L_0x000b
        L_0x0049:
            com.fasterxml.jackson.core.filter.TokenFilterContext r3 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r3.createChildArrayContext(r0, r2)
            r6._headContext = r0
            goto L_0x0002
        L_0x0052:
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r6._itemFilter
            com.fasterxml.jackson.core.filter.TokenFilter r4 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 != r4) goto L_0x0062
            com.fasterxml.jackson.core.filter.TokenFilterContext r2 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r2.createChildObjectContext(r0, r1)
            r6._headContext = r0
            r0 = r3
            goto L_0x000b
        L_0x0062:
            if (r0 != 0) goto L_0x006a
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            r0.skipChildren()
            goto L_0x0002
        L_0x006a:
            com.fasterxml.jackson.core.filter.TokenFilterContext r3 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r3.checkValue(r0)
            if (r0 != 0) goto L_0x0078
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            r0.skipChildren()
            goto L_0x0002
        L_0x0078:
            com.fasterxml.jackson.core.filter.TokenFilter r3 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 == r3) goto L_0x0080
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r0.filterStartObject()
        L_0x0080:
            r6._itemFilter = r0
            com.fasterxml.jackson.core.filter.TokenFilter r3 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 != r3) goto L_0x0094
            com.fasterxml.jackson.core.filter.TokenFilterContext r2 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r2.createChildObjectContext(r0, r1)
            r6._headContext = r0
            com.fasterxml.jackson.core.JsonToken r0 = r6._nextBuffered(r7)
            goto L_0x000b
        L_0x0094:
            com.fasterxml.jackson.core.filter.TokenFilterContext r3 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r3.createChildObjectContext(r0, r2)
            r6._headContext = r0
            goto L_0x0002
        L_0x009e:
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r0.getFilter()
            if (r0 == 0) goto L_0x00ad
            com.fasterxml.jackson.core.filter.TokenFilter r4 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 == r4) goto L_0x00ad
            r0.filterFinishArray()
        L_0x00ad:
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r6._headContext
            if (r0 != r7) goto L_0x00d2
            r4 = r1
        L_0x00b2:
            if (r4 == 0) goto L_0x00d4
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r6._headContext
            boolean r0 = r0.isStartHandled()
            if (r0 == 0) goto L_0x00d4
            r0 = r1
        L_0x00bd:
            com.fasterxml.jackson.core.filter.TokenFilterContext r5 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilterContext r5 = r5.getParent()
            r6._headContext = r5
            com.fasterxml.jackson.core.filter.TokenFilterContext r5 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilter r5 = r5.getFilter()
            r6._itemFilter = r5
            if (r0 == 0) goto L_0x00d6
            r0 = r3
            goto L_0x000b
        L_0x00d2:
            r4 = r2
            goto L_0x00b2
        L_0x00d4:
            r0 = r2
            goto L_0x00bd
        L_0x00d6:
            if (r4 != 0) goto L_0x00dc
            com.fasterxml.jackson.core.filter.TokenFilterContext r0 = r6._headContext
            if (r0 != r7) goto L_0x0002
        L_0x00dc:
            r0 = 0
            goto L_0x000b
        L_0x00df:
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            java.lang.String r0 = r0.getCurrentName()
            com.fasterxml.jackson.core.filter.TokenFilterContext r3 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilter r3 = r3.setFieldName(r0)
            com.fasterxml.jackson.core.filter.TokenFilter r4 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r3 != r4) goto L_0x00f7
            r6._itemFilter = r3
            com.fasterxml.jackson.core.JsonToken r0 = r6._nextBuffered(r7)
            goto L_0x000b
        L_0x00f7:
            if (r3 != 0) goto L_0x0105
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            r0.nextToken()
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            r0.skipChildren()
            goto L_0x0002
        L_0x0105:
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r3.includeProperty(r0)
            if (r0 != 0) goto L_0x0117
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            r0.nextToken()
            com.fasterxml.jackson.core.JsonParser r0 = r6.delegate
            r0.skipChildren()
            goto L_0x0002
        L_0x0117:
            r6._itemFilter = r0
            com.fasterxml.jackson.core.filter.TokenFilter r3 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 != r3) goto L_0x0002
            com.fasterxml.jackson.core.JsonToken r0 = r6._nextBuffered(r7)
            goto L_0x000b
        L_0x0123:
            if (r0 == 0) goto L_0x0002
            com.fasterxml.jackson.core.filter.TokenFilterContext r3 = r6._headContext
            com.fasterxml.jackson.core.filter.TokenFilter r0 = r3.checkValue(r0)
            com.fasterxml.jackson.core.filter.TokenFilter r3 = com.fasterxml.jackson.core.filter.TokenFilter.INCLUDE_ALL
            if (r0 == r3) goto L_0x0139
            if (r0 == 0) goto L_0x0002
            com.fasterxml.jackson.core.JsonParser r3 = r6.delegate
            boolean r0 = r0.includeValue(r3)
            if (r0 == 0) goto L_0x0002
        L_0x0139:
            com.fasterxml.jackson.core.JsonToken r0 = r6._nextBuffered(r7)
            goto L_0x000b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.filter.FilteringParserDelegate._nextTokenWithBuffering(com.fasterxml.jackson.core.filter.TokenFilterContext):com.fasterxml.jackson.core.JsonToken");
    }

    private JsonToken _nextBuffered(TokenFilterContext tokenFilterContext) throws IOException {
        this._exposedContext = tokenFilterContext;
        JsonToken nextTokenToRead = tokenFilterContext.nextTokenToRead();
        if (nextTokenToRead != null) {
            return nextTokenToRead;
        }
        while (tokenFilterContext != this._headContext) {
            tokenFilterContext = this._exposedContext.findChildOf(tokenFilterContext);
            this._exposedContext = tokenFilterContext;
            if (tokenFilterContext == null) {
                throw _constructError("Unexpected problem: chain of filtered context broken");
            }
            nextTokenToRead = this._exposedContext.nextTokenToRead();
            if (nextTokenToRead != null) {
            }
        }
        throw _constructError("Internal error: failed to locate expected buffered tokens");
        return nextTokenToRead;
    }

    public JsonToken nextValue() throws IOException {
        JsonToken nextToken = nextToken();
        if (nextToken == JsonToken.FIELD_NAME) {
            return nextToken();
        }
        return nextToken;
    }

    public JsonParser skipChildren() throws IOException {
        if (this._currToken == JsonToken.START_OBJECT || this._currToken == JsonToken.START_ARRAY) {
            int i = 1;
            while (true) {
                JsonToken nextToken = nextToken();
                if (nextToken == null) {
                    break;
                } else if (nextToken.isStructStart()) {
                    i++;
                } else if (nextToken.isStructEnd()) {
                    i--;
                    if (i == 0) {
                        break;
                    }
                } else {
                    continue;
                }
            }
        }
        return this;
    }

    public String getText() throws IOException {
        return this.delegate.getText();
    }

    public boolean hasTextCharacters() {
        return this.delegate.hasTextCharacters();
    }

    public char[] getTextCharacters() throws IOException {
        return this.delegate.getTextCharacters();
    }

    public int getTextLength() throws IOException {
        return this.delegate.getTextLength();
    }

    public int getTextOffset() throws IOException {
        return this.delegate.getTextOffset();
    }

    public BigInteger getBigIntegerValue() throws IOException {
        return this.delegate.getBigIntegerValue();
    }

    public boolean getBooleanValue() throws IOException {
        return this.delegate.getBooleanValue();
    }

    public byte getByteValue() throws IOException {
        return this.delegate.getByteValue();
    }

    public short getShortValue() throws IOException {
        return this.delegate.getShortValue();
    }

    public BigDecimal getDecimalValue() throws IOException {
        return this.delegate.getDecimalValue();
    }

    public double getDoubleValue() throws IOException {
        return this.delegate.getDoubleValue();
    }

    public float getFloatValue() throws IOException {
        return this.delegate.getFloatValue();
    }

    public int getIntValue() throws IOException {
        return this.delegate.getIntValue();
    }

    public long getLongValue() throws IOException {
        return this.delegate.getLongValue();
    }

    public NumberType getNumberType() throws IOException {
        return this.delegate.getNumberType();
    }

    public Number getNumberValue() throws IOException {
        return this.delegate.getNumberValue();
    }

    public int getValueAsInt() throws IOException {
        return this.delegate.getValueAsInt();
    }

    public int getValueAsInt(int i) throws IOException {
        return this.delegate.getValueAsInt(i);
    }

    public long getValueAsLong() throws IOException {
        return this.delegate.getValueAsLong();
    }

    public long getValueAsLong(long j) throws IOException {
        return this.delegate.getValueAsLong(j);
    }

    public double getValueAsDouble() throws IOException {
        return this.delegate.getValueAsDouble();
    }

    public double getValueAsDouble(double d) throws IOException {
        return this.delegate.getValueAsDouble(d);
    }

    public boolean getValueAsBoolean() throws IOException {
        return this.delegate.getValueAsBoolean();
    }

    public boolean getValueAsBoolean(boolean z) throws IOException {
        return this.delegate.getValueAsBoolean(z);
    }

    public String getValueAsString() throws IOException {
        return this.delegate.getValueAsString();
    }

    public String getValueAsString(String str) throws IOException {
        return this.delegate.getValueAsString(str);
    }

    public Object getEmbeddedObject() throws IOException {
        return this.delegate.getEmbeddedObject();
    }

    public byte[] getBinaryValue(Base64Variant base64Variant) throws IOException {
        return this.delegate.getBinaryValue(base64Variant);
    }

    public int readBinaryValue(Base64Variant base64Variant, OutputStream outputStream) throws IOException {
        return this.delegate.readBinaryValue(base64Variant, outputStream);
    }

    public JsonLocation getTokenLocation() {
        return this.delegate.getTokenLocation();
    }

    /* access modifiers changed from: protected */
    public JsonStreamContext _filterContext() {
        if (this._exposedContext != null) {
            return this._exposedContext;
        }
        return this._headContext;
    }
}
