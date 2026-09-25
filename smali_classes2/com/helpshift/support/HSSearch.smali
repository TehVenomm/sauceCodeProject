.class public final Lcom/helpshift/support/HSSearch;
.super Ljava/lang/Object;
.source "HSSearch.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;
    }
.end annotation


# static fields
.field private static characterTable:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static indexing:Z

.field private static markDeinit:Z

.field private static metaPhone:Lcom/helpshift/support/external/DoubleMetaphone;

.field private static final patternGenerateToken:Ljava/util/regex/Pattern;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "[a-zA-Z0-9]+"

    .line 35
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/HSSearch;->patternGenerateToken:Ljava/util/regex/Pattern;

    .line 36
    new-instance v0, Lcom/helpshift/support/external/DoubleMetaphone;

    invoke-direct {v0}, Lcom/helpshift/support/external/DoubleMetaphone;-><init>()V

    sput-object v0, Lcom/helpshift/support/HSSearch;->metaPhone:Lcom/helpshift/support/external/DoubleMetaphone;

    const/4 v0, 0x0

    .line 37
    sput-boolean v0, Lcom/helpshift/support/HSSearch;->indexing:Z

    .line 38
    sput-boolean v0, Lcom/helpshift/support/HSSearch;->markDeinit:Z

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method protected static buildFuzzyIndex(Ljava/util/ArrayList;)Ljava/util/Map;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/Faq;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/model/FuzzySearchToken;",
            ">;>;"
        }
    .end annotation

    .line 330
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 332
    invoke-virtual {p0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p0

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/support/Faq;

    .line 333
    iget-object v3, v3, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    .line 334
    invoke-static {v3}, Lcom/helpshift/support/HSSearch;->sanitize(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Lcom/helpshift/support/HSSearch;->generateTokens(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_0
    :goto_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_3

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 335
    invoke-virtual {v4}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v4

    .line 336
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v5

    const/4 v6, 0x3

    if-le v5, v6, :cond_0

    .line 337
    new-instance v5, Lcom/helpshift/support/model/FuzzySearchToken;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v7, ""

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v5, v4, v6}, Lcom/helpshift/support/model/FuzzySearchToken;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v6, 0x1

    .line 338
    invoke-virtual {v4, v1, v6}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v7

    .line 339
    invoke-interface {v0, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/util/List;

    if-nez v8, :cond_1

    .line 341
    new-instance v8, Ljava/util/ArrayList;

    invoke-direct {v8}, Ljava/util/ArrayList;-><init>()V

    .line 343
    :cond_1
    invoke-interface {v8, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 344
    invoke-interface {v0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const/4 v7, 0x2

    .line 346
    invoke-virtual {v4, v6, v7}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    .line 347
    invoke-interface {v0, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/util/List;

    if-nez v6, :cond_2

    .line 349
    new-instance v6, Ljava/util/ArrayList;

    invoke-direct {v6}, Ljava/util/ArrayList;-><init>()V

    .line 351
    :cond_2
    invoke-interface {v6, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 352
    invoke-interface {v0, v4, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    :cond_3
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_4
    return-object v0
.end method

.method private static buildPageIndexTrie(Lcom/helpshift/support/search/tfidf/PageIndexTrie;Ljava/lang/String;Ljava/lang/String;Ljava/util/List;I)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/support/search/tfidf/PageIndexTrie;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;I)V"
        }
    .end annotation

    .line 148
    invoke-static {p1}, Lcom/helpshift/support/HSSearch;->sanitize(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/support/HSSearch;->generateTokens(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p1

    const/16 v0, 0x14

    invoke-static {p0, p1, v0, p4}, Lcom/helpshift/support/HSSearch;->generateAndAddString(Lcom/helpshift/support/search/tfidf/PageIndexTrie;Ljava/util/List;II)V

    .line 149
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    .line 150
    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :goto_0
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 151
    invoke-static {v0}, Lcom/helpshift/support/HSSearch;->generateTokens(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v0

    invoke-interface {p1, v0}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    goto :goto_0

    :cond_0
    const/16 p3, 0x1e

    .line 153
    invoke-static {p0, p1, p3, p4}, Lcom/helpshift/support/HSSearch;->generateAndAddString(Lcom/helpshift/support/search/tfidf/PageIndexTrie;Ljava/util/List;II)V

    .line 154
    invoke-static {p2}, Lcom/helpshift/support/HSSearch;->sanitize(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/support/HSSearch;->generateTokens(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p1

    const/16 p2, 0xa

    invoke-static {p0, p1, p2, p4}, Lcom/helpshift/support/HSSearch;->generateAndAddString(Lcom/helpshift/support/search/tfidf/PageIndexTrie;Ljava/util/List;II)V

    return-void
.end method

.method protected static buildTfidfIndex(Ljava/util/ArrayList;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/Faq;",
            ">;)V"
        }
    .end annotation

    .line 320
    invoke-virtual {p0}, Ljava/util/ArrayList;->size()I

    move-result v0

    .line 321
    new-instance v1, Lcom/helpshift/support/search/tfidf/PageIndexTrie;

    invoke-direct {v1, v0}, Lcom/helpshift/support/search/tfidf/PageIndexTrie;-><init>(I)V

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_0

    .line 323
    invoke-virtual {p0, v2}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/support/Faq;

    .line 324
    iget-object v4, v3, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    iget-object v5, v3, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    invoke-virtual {v3}, Lcom/helpshift/support/Faq;->getTags()Ljava/util/List;

    move-result-object v3

    invoke-static {v1, v4, v5, v3, v2}, Lcom/helpshift/support/HSSearch;->buildPageIndexTrie(Lcom/helpshift/support/search/tfidf/PageIndexTrie;Ljava/lang/String;Ljava/lang/String;Ljava/util/List;I)V

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 326
    :cond_0
    invoke-virtual {v1}, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->createAndStoreTfIdfIndex()V

    return-void
.end method

.method public static calcFreq(II)I
    .locals 2

    const/4 v0, 0x1

    if-ne p0, v0, :cond_0

    const/4 p0, 0x5

    return p0

    :cond_0
    const/16 v1, 0x28

    if-ne v1, p1, :cond_1

    return p0

    :cond_1
    const/16 p0, 0xa

    const/16 v1, 0x1e

    if-ne p0, p1, :cond_2

    return v1

    :cond_2
    const/16 p0, 0x32

    if-ne p0, p1, :cond_3

    return v0

    :cond_3
    const/16 p0, 0x14

    if-ne p0, p1, :cond_4

    const/16 p0, 0x12c

    return p0

    :cond_4
    if-ne v1, p1, :cond_5

    const/16 p0, 0x96

    return p0

    :cond_5
    return v0
.end method

.method static calculateWordDistance(Ljava/lang/String;Ljava/lang/String;)F
    .locals 17

    .line 444
    invoke-virtual/range {p0 .. p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    .line 445
    invoke-virtual/range {p1 .. p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    .line 447
    invoke-virtual {v0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v0

    .line 448
    invoke-virtual {v1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v1

    .line 452
    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v2

    .line 453
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    add-int/lit8 v4, v2, 0x1

    if-eqz v2, :cond_7

    add-int/lit8 v2, v3, 0x1

    if-eqz v3, :cond_7

    mul-int v3, v4, v2

    .line 456
    new-array v5, v3, [I

    const/4 v7, 0x0

    :goto_0
    if-ge v7, v4, :cond_0

    .line 459
    aput v7, v5, v7

    add-int/lit8 v7, v7, 0x1

    goto :goto_0

    :cond_0
    const/4 v7, 0x0

    :goto_1
    if-ge v7, v2, :cond_1

    mul-int v8, v7, v4

    .line 463
    aput v7, v5, v8

    add-int/lit8 v7, v7, 0x1

    goto :goto_1

    :cond_1
    const/4 v7, 0x1

    const/4 v8, 0x1

    :goto_2
    if-ge v8, v4, :cond_5

    const/4 v9, 0x1

    :goto_3
    if-ge v9, v2, :cond_4

    add-int/lit8 v10, v8, -0x1

    .line 468
    invoke-virtual {v0, v10}, Ljava/lang/String;->charAt(I)C

    move-result v11

    add-int/lit8 v12, v9, -0x1

    invoke-virtual {v1, v12}, Ljava/lang/String;->charAt(I)C

    move-result v13

    if-ne v11, v13, :cond_2

    const/4 v11, 0x0

    goto :goto_4

    :cond_2
    const/4 v11, 0x1

    :goto_4
    mul-int v13, v9, v4

    add-int/2addr v13, v8

    mul-int v14, v12, v4

    add-int/2addr v14, v8

    .line 475
    aget v15, v5, v14

    add-int/2addr v15, v7

    add-int/lit8 v16, v13, -0x1

    aget v16, v5, v16

    add-int/lit8 v6, v16, 0x1

    sub-int/2addr v14, v7

    aget v14, v5, v14

    add-int/2addr v14, v11

    invoke-static {v15, v6, v14}, Lcom/helpshift/support/HSSearch;->smallestOf(III)I

    move-result v6

    aput v6, v5, v13

    if-le v8, v7, :cond_3

    if-le v9, v7, :cond_3

    .line 480
    invoke-virtual {v0, v10}, Ljava/lang/String;->charAt(I)C

    move-result v6

    add-int/lit8 v10, v9, -0x2

    invoke-virtual {v1, v10}, Ljava/lang/String;->charAt(I)C

    move-result v14

    if-ne v6, v14, :cond_3

    add-int/lit8 v6, v8, -0x2

    .line 481
    invoke-virtual {v0, v6}, Ljava/lang/String;->charAt(I)C

    move-result v6

    invoke-virtual {v1, v12}, Ljava/lang/String;->charAt(I)C

    move-result v12

    if-ne v6, v12, :cond_3

    .line 482
    aget v6, v5, v13

    mul-int v10, v10, v4

    add-int/2addr v10, v8

    add-int/lit8 v10, v10, -0x2

    aget v10, v5, v10

    add-int/2addr v10, v11

    invoke-static {v6, v10}, Lcom/helpshift/support/HSSearch;->smallestOf(II)I

    move-result v6

    aput v6, v5, v13

    :cond_3
    add-int/lit8 v9, v9, 0x1

    goto :goto_3

    :cond_4
    add-int/lit8 v8, v8, 0x1

    goto :goto_2

    :cond_5
    sub-int/2addr v3, v7

    .line 487
    aget v0, v5, v3

    if-le v4, v2, :cond_6

    move v2, v4

    :cond_6
    const/high16 v1, 0x3f800000    # 1.0f

    int-to-float v0, v0

    int-to-float v2, v2

    div-float/2addr v0, v2

    sub-float/2addr v1, v0

    return v1

    :cond_7
    const/4 v0, 0x0

    return v0
.end method

.method public static deinit()V
    .locals 1

    .line 54
    sget-boolean v0, Lcom/helpshift/support/HSSearch;->indexing:Z

    if-nez v0, :cond_0

    .line 55
    invoke-static {}, Lcom/helpshift/support/util/HSTransliterator;->deinit()V

    goto :goto_0

    :cond_0
    const/4 v0, 0x1

    .line 58
    sput-boolean v0, Lcom/helpshift/support/HSSearch;->markDeinit:Z

    :goto_0
    const/4 v0, 0x0

    .line 60
    sput-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    return-void
.end method

.method public static escapeHtml(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 64
    invoke-static {p0}, Lcom/helpshift/support/util/HtmlUtil;->fromHtml(Ljava/lang/String;)Landroid/text/Spanned;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method protected static filterSearchQuery(Ljava/util/ArrayList;Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;)Ljava/util/ArrayList;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/model/TfIdfSearchToken;",
            ">;",
            "Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;",
            ")",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/model/TfIdfSearchToken;",
            ">;"
        }
    .end annotation

    .line 190
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 191
    invoke-virtual {p0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :cond_0
    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_4

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/model/TfIdfSearchToken;

    .line 192
    iget v2, v1, Lcom/helpshift/support/model/TfIdfSearchToken;->type:I

    .line 193
    sget-object v3, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->FULL_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    if-ne p1, v3, :cond_1

    .line 194
    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 196
    :cond_1
    sget-object v3, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->METAPHONE_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    if-ne p1, v3, :cond_2

    const/16 v3, 0x32

    if-ne v3, v2, :cond_2

    .line 197
    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 199
    :cond_2
    sget-object v3, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->KEYWORD_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    if-ne p1, v3, :cond_0

    const/16 v3, 0xa

    if-eq v3, v2, :cond_3

    const/16 v3, 0x28

    if-ne v3, v2, :cond_0

    .line 200
    :cond_3
    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_4
    return-object v0
.end method

.method private static generateAndAddString(Lcom/helpshift/support/search/tfidf/PageIndexTrie;Ljava/util/List;II)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/support/search/tfidf/PageIndexTrie;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;II)V"
        }
    .end annotation

    .line 158
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 159
    invoke-static {v0, p2}, Lcom/helpshift/support/HSSearch;->generateVariations(Ljava/lang/String;I)Ljava/util/ArrayList;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/model/TfIdfSearchToken;

    .line 160
    iget-object v2, v1, Lcom/helpshift/support/model/TfIdfSearchToken;->value:Ljava/lang/String;

    iget v1, v1, Lcom/helpshift/support/model/TfIdfSearchToken;->type:I

    invoke-virtual {p0, v2, v1, p3}, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->insert(Ljava/lang/String;II)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method public static generateSearchVariations(Ljava/lang/String;)Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/model/TfIdfSearchToken;",
            ">;"
        }
    .end annotation

    const/16 v0, 0xa

    .line 106
    invoke-static {p0, v0}, Lcom/helpshift/support/HSSearch;->generateVariations(Ljava/lang/String;I)Ljava/util/ArrayList;

    move-result-object p0

    return-object p0
.end method

.method public static generateTokens(Ljava/lang/String;)Ljava/util/ArrayList;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 72
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 73
    sget-object v1, Lcom/helpshift/support/HSSearch;->patternGenerateToken:Ljava/util/regex/Pattern;

    invoke-virtual {v1, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    .line 74
    :cond_0
    :goto_0
    invoke-virtual {p0}, Ljava/util/regex/Matcher;->find()Z

    move-result v1

    if-eqz v1, :cond_1

    const/4 v1, 0x0

    .line 75
    invoke-virtual {p0, v1}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    const/4 v3, 0x2

    if-le v2, v3, :cond_0

    .line 76
    invoke-virtual {p0, v1}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method public static generateTokensForSearchQuery(Ljava/lang/String;)Ljava/util/ArrayList;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 83
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 84
    sget-object v1, Lcom/helpshift/support/HSSearch;->patternGenerateToken:Ljava/util/regex/Pattern;

    invoke-virtual {v1, p0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v1

    .line 85
    :cond_0
    :goto_0
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->find()Z

    move-result v2

    if-eqz v2, :cond_2

    const/4 v2, 0x0

    .line 86
    invoke-virtual {v1, v2}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v3

    const/4 v4, 0x2

    if-gt v3, v4, :cond_1

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v3

    if-le v3, v4, :cond_0

    .line 87
    :cond_1
    invoke-virtual {v1, v2}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_2
    return-object v0
.end method

.method public static generateVariations(Ljava/lang/String;I)Ljava/util/ArrayList;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "I)",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/model/TfIdfSearchToken;",
            ">;"
        }
    .end annotation

    .line 94
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    .line 96
    new-instance v1, Lcom/helpshift/support/model/TfIdfSearchToken;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/support/model/TfIdfSearchToken;-><init>(Ljava/lang/String;I)V

    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    .line 98
    sget-object p1, Lcom/helpshift/support/HSSearch;->metaPhone:Lcom/helpshift/support/external/DoubleMetaphone;

    const/4 v1, 0x0

    invoke-virtual {p1, p0, v1}, Lcom/helpshift/support/external/DoubleMetaphone;->doubleMetaphone(Ljava/lang/String;Z)Ljava/lang/String;

    move-result-object p0

    if-eqz p0, :cond_0

    .line 100
    new-instance p1, Lcom/helpshift/support/model/TfIdfSearchToken;

    invoke-virtual {p0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p0

    const/16 v1, 0x32

    invoke-direct {p1, p0, v1}, Lcom/helpshift/support/model/TfIdfSearchToken;-><init>(Ljava/lang/String;I)V

    invoke-virtual {v0, p1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    .line 102
    :cond_0
    new-instance p0, Ljava/util/ArrayList;

    invoke-direct {p0, v0}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object p0
.end method

.method static getCharacterTable()Ljava/util/HashMap;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 410
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    if-nez v0, :cond_0

    .line 411
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    .line 412
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "a"

    const-string v2, "q"

    const-string v3, "w"

    const-string v4, "s"

    const-string v5, "z"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 413
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "b"

    const-string v2, "v"

    const-string v3, "h"

    const-string v4, "n"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 414
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "c"

    const-string v2, "x"

    const-string v3, "f"

    const-string v4, "v"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 415
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "d"

    const-string v2, "s"

    const-string v3, "z"

    const-string v4, "x"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 416
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "e"

    const-string v2, "w"

    const-string v3, "s"

    const-string v4, "d"

    const-string v5, "r"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 417
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "f"

    const-string v2, "d"

    const-string v3, "g"

    const-string v4, "c"

    const-string v5, "x"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 418
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "g"

    const-string v2, "h"

    const-string v3, "f"

    const-string v4, "v"

    const-string v5, "b"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 419
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "h"

    const-string v2, "g"

    const-string v3, "j"

    const-string v4, "b"

    const-string v5, "n"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 420
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "i"

    const-string v2, "u"

    const-string v3, "o"

    const-string v4, "k"

    const-string v5, "j"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 421
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "j"

    const-string v2, "m"

    const-string v3, "n"

    const-string v4, "h"

    const-string v5, "k"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 422
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "k"

    const-string v2, "j"

    const-string v3, "l"

    const-string v4, "m"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 423
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "l"

    const-string v2, "k"

    const-string v3, "p"

    const-string v4, "m"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 424
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "m"

    const-string v2, "n"

    const-string v3, "b"

    const-string v4, "l"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 425
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "n"

    const-string v2, "b"

    const-string v3, "j"

    const-string v4, "m"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 426
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "o"

    const-string v2, "l"

    const-string v3, "k"

    const-string v4, "p"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 427
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "p"

    const-string v2, "l"

    const-string v3, "o"

    filled-new-array {v2, v3}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 428
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "q"

    const-string v2, "w"

    const-string v3, "a"

    filled-new-array {v2, v3}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 429
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "r"

    const-string v2, "s"

    const-string v3, "d"

    const-string v4, "e"

    const-string v5, "f"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 430
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "s"

    const-string v2, "a"

    const-string v3, "z"

    const-string v4, "d"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 431
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "t"

    const-string v2, "r"

    const-string v3, "f"

    const-string v4, "g"

    const-string v5, "y"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 432
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "u"

    const-string v2, "j"

    const-string v3, "h"

    const-string v4, "i"

    const-string v5, "y"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 433
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "v"

    const-string v2, "c"

    const-string v3, "g"

    const-string v4, "b"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 434
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "w"

    const-string v2, "q"

    const-string v3, "a"

    const-string v4, "s"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 435
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "x"

    const-string v2, "z"

    const-string v3, "s"

    const-string v4, "c"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 436
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "y"

    const-string v2, "g"

    const-string v3, "h"

    const-string v4, "t"

    const-string v5, "u"

    filled-new-array {v2, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 437
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    const-string v1, "z"

    const-string v2, "a"

    const-string v3, "s"

    const-string v4, "x"

    filled-new-array {v2, v3, v4}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 439
    :cond_0
    sget-object v0, Lcom/helpshift/support/HSSearch;->characterTable:Ljava/util/HashMap;

    return-object v0
.end method

.method public static getFuzzyMatches(Ljava/lang/String;Ljava/util/Map;)Ljava/util/ArrayList;
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/model/FuzzySearchToken;",
            ">;>;)",
            "Ljava/util/ArrayList<",
            "Ljava/util/HashMap;",
            ">;"
        }
    .end annotation

    .line 362
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    if-eqz p1, :cond_5

    .line 364
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 365
    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->sanitize(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->generateTokens(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p0

    invoke-virtual {p0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :cond_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_4

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    const/4 v3, 0x0

    const/4 v4, 0x1

    .line 366
    invoke-virtual {v2, v3, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    .line 367
    new-instance v4, Ljava/util/ArrayList;

    invoke-static {v3}, Lcom/helpshift/support/HSSearch;->getNeighbourCharacters(Ljava/lang/String;)Ljava/util/List;

    move-result-object v5

    invoke-direct {v4, v5}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 368
    invoke-interface {v4, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 369
    invoke-interface {v4}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 370
    invoke-interface {p1, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/ArrayList;

    if-eqz v4, :cond_1

    .line 372
    invoke-virtual {v4}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_2
    :goto_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/support/model/FuzzySearchToken;

    .line 373
    iget-object v6, v5, Lcom/helpshift/support/model/FuzzySearchToken;->word:Ljava/lang/String;

    .line 374
    invoke-static {v6, v2}, Lcom/helpshift/support/HSSearch;->calculateWordDistance(Ljava/lang/String;Ljava/lang/String;)F

    move-result v7

    float-to-double v7, v7

    const-wide v9, 0x3fe6666666666666L    # 0.7

    cmpl-double v11, v7, v9

    if-lez v11, :cond_2

    .line 376
    iget-object v5, v5, Lcom/helpshift/support/model/FuzzySearchToken;->docId:Ljava/lang/String;

    .line 377
    invoke-virtual {v1, v5}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/util/ArrayList;

    if-nez v7, :cond_3

    .line 379
    new-instance v7, Ljava/util/ArrayList;

    invoke-direct {v7}, Ljava/util/ArrayList;-><init>()V

    .line 381
    :cond_3
    invoke-virtual {v7, v6}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 382
    invoke-virtual {v1, v5, v7}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 389
    :cond_4
    invoke-virtual {v1}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object p0

    invoke-interface {p0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :goto_1
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result p1

    if-eqz p1, :cond_5

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/Map$Entry;

    .line 390
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "f"

    .line 391
    invoke-interface {p1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "t"

    .line 392
    invoke-interface {p1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object p1

    invoke-virtual {v1, v2, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 393
    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_1

    :cond_5
    return-object v0
.end method

.method private static getNeighbourCharacters(Ljava/lang/String;)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 400
    invoke-static {}, Lcom/helpshift/support/HSSearch;->getCharacterTable()Ljava/util/HashMap;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 401
    invoke-virtual {v0, p0}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 402
    invoke-virtual {v0, p0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, [Ljava/lang/Object;

    invoke-static {p0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p0

    return-object p0

    .line 405
    :cond_0
    new-instance p0, Ljava/util/ArrayList;

    invoke-direct {p0}, Ljava/util/ArrayList;-><init>()V

    return-object p0
.end method

.method public static getTermWeight(I)I
    .locals 1

    const/16 v0, 0x14

    if-ne p0, v0, :cond_0

    const/4 p0, 0x5

    goto :goto_0

    :cond_0
    const/16 v0, 0x1e

    if-ne p0, v0, :cond_1

    const/4 p0, 0x3

    goto :goto_0

    :cond_1
    const/4 p0, 0x1

    :goto_0
    return p0
.end method

.method public static indexDocuments(Ljava/util/ArrayList;)Lcom/helpshift/support/model/FaqSearchIndex;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/Faq;",
            ">;)",
            "Lcom/helpshift/support/model/FaqSearchIndex;"
        }
    .end annotation

    .line 166
    sget-boolean v0, Lcom/helpshift/support/HSSearch;->indexing:Z

    if-eqz v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 170
    :cond_0
    invoke-static {}, Lcom/helpshift/support/util/HSTransliterator;->isLoaded()Z

    move-result v0

    const/4 v1, 0x1

    if-nez v0, :cond_1

    .line 171
    invoke-static {}, Lcom/helpshift/support/util/HSTransliterator;->init()V

    .line 172
    sput-boolean v1, Lcom/helpshift/support/HSSearch;->markDeinit:Z

    .line 175
    :cond_1
    sput-boolean v1, Lcom/helpshift/support/HSSearch;->indexing:Z

    .line 176
    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->buildFuzzyIndex(Ljava/util/ArrayList;)Ljava/util/Map;

    move-result-object v0

    .line 177
    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->buildTfidfIndex(Ljava/util/ArrayList;)V

    .line 178
    new-instance p0, Lcom/helpshift/support/model/FaqSearchIndex;

    invoke-direct {p0, v0}, Lcom/helpshift/support/model/FaqSearchIndex;-><init>(Ljava/util/Map;)V

    const/4 v0, 0x0

    .line 180
    sput-boolean v0, Lcom/helpshift/support/HSSearch;->indexing:Z

    .line 181
    sget-boolean v1, Lcom/helpshift/support/HSSearch;->markDeinit:Z

    if-eqz v1, :cond_2

    .line 182
    invoke-static {}, Lcom/helpshift/support/HSSearch;->deinit()V

    .line 183
    sput-boolean v0, Lcom/helpshift/support/HSSearch;->markDeinit:Z

    :cond_2
    return-object p0
.end method

.method public static init()V
    .locals 3

    .line 42
    sget-boolean v0, Lcom/helpshift/support/HSSearch;->indexing:Z

    if-nez v0, :cond_0

    .line 43
    new-instance v0, Ljava/lang/Thread;

    new-instance v1, Lcom/helpshift/support/HSSearch$1;

    invoke-direct {v1}, Lcom/helpshift/support/HSSearch$1;-><init>()V

    const-string v2, "HS-trnsltrtr"

    invoke-direct {v0, v1, v2}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;Ljava/lang/String;)V

    const/4 v1, 0x1

    .line 48
    invoke-virtual {v0, v1}, Ljava/lang/Thread;->setDaemon(Z)V

    .line 49
    invoke-virtual {v0}, Ljava/lang/Thread;->start()V

    :cond_0
    return-void
.end method

.method private static isMetaTypeMatching(II)Z
    .locals 2

    const/4 v0, 0x1

    const/16 v1, 0x32

    if-eq v1, p0, :cond_1

    if-ne v1, p1, :cond_0

    goto :goto_0

    :cond_0
    return v0

    :cond_1
    :goto_0
    if-ne p0, p1, :cond_2

    goto :goto_1

    :cond_2
    const/4 v0, 0x0

    :goto_1
    return v0
.end method

.method public static queryDocs(Ljava/lang/String;Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;)Ljava/util/ArrayList;
    .locals 14
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;",
            ")",
            "Ljava/util/ArrayList<",
            "Ljava/util/HashMap;",
            ">;"
        }
    .end annotation

    .line 208
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 210
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 212
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 213
    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->sanitize(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->generateTokensForSearchQuery(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p0

    invoke-virtual {p0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 214
    invoke-static {v3}, Lcom/helpshift/support/HSSearch;->generateSearchVariations(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v3

    invoke-static {v3, p1}, Lcom/helpshift/support/HSSearch;->filterSearchQuery(Ljava/util/ArrayList;Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;)Ljava/util/ArrayList;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/util/ArrayList;->addAll(Ljava/util/Collection;)Z

    goto :goto_0

    .line 217
    :cond_0
    invoke-static {}, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->getInstance()Lcom/helpshift/support/search/SearchTokenDao;

    move-result-object p0

    const/4 p1, 0x0

    if-eqz p0, :cond_8

    .line 219
    invoke-virtual {v2}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_1
    :goto_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_8

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/support/model/TfIdfSearchToken;

    .line 220
    iget-object v4, v3, Lcom/helpshift/support/model/TfIdfSearchToken;->value:Ljava/lang/String;

    .line 221
    iget v5, v3, Lcom/helpshift/support/model/TfIdfSearchToken;->type:I

    .line 222
    invoke-interface {p0, v4}, Lcom/helpshift/support/search/SearchTokenDao;->get(Ljava/lang/String;)Lcom/helpshift/support/search/SearchTokenDto;

    move-result-object v6

    if-eqz v6, :cond_1

    .line 224
    iget v7, v6, Lcom/helpshift/support/search/SearchTokenDto;->wordType:I

    iget v3, v3, Lcom/helpshift/support/model/TfIdfSearchToken;->type:I

    invoke-static {v7, v3}, Lcom/helpshift/support/HSSearch;->isMetaTypeMatching(II)Z

    move-result v3

    if-eqz v3, :cond_1

    .line 225
    iget-object v3, v6, Lcom/helpshift/support/search/SearchTokenDto;->scoreMap:Ljava/util/Map;

    .line 226
    new-instance v6, Ljava/util/HashMap;

    invoke-direct {v6}, Ljava/util/HashMap;-><init>()V

    .line 227
    invoke-interface {v3}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_2
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_2

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/util/Map$Entry;

    .line 228
    invoke-interface {v7}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v8

    invoke-static {v8}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v8

    invoke-interface {v7}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v7

    invoke-interface {v6, v8, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_2

    .line 231
    :cond_2
    invoke-interface {v6}, Ljava/util/Map;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_1

    .line 232
    invoke-interface {v6}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_3
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_6

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/util/Map$Entry;

    .line 233
    invoke-interface {v7}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/String;

    .line 234
    invoke-virtual {v1, v8}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/util/ArrayList;

    if-nez v9, :cond_3

    .line 236
    new-instance v9, Ljava/util/ArrayList;

    invoke-direct {v9}, Ljava/util/ArrayList;-><init>()V

    .line 238
    :cond_3
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v10

    if-lez v10, :cond_4

    .line 239
    invoke-virtual {v9, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 241
    :cond_4
    invoke-virtual {v1, v8, v9}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 242
    invoke-interface {v7}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/Double;

    .line 243
    invoke-virtual {v0, v8}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/lang/Double;

    .line 244
    invoke-virtual {v7}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v10

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v7

    invoke-static {v7, v5}, Lcom/helpshift/support/HSSearch;->calcFreq(II)I

    move-result v7

    int-to-double v12, v7

    invoke-static {v12, v13}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v10, v10, v12

    invoke-static {v10, v11}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v7

    if-eqz v9, :cond_5

    .line 246
    invoke-virtual {v9}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v9

    invoke-virtual {v7}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v11

    add-double/2addr v9, v11

    invoke-static {v9, v10}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v7

    invoke-virtual {v0, v8, v7}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_3

    .line 249
    :cond_5
    invoke-virtual {v0, v8, v7}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_3

    .line 254
    :cond_6
    new-instance v3, Ljava/util/HashSet;

    invoke-interface {v6}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v4

    invoke-direct {v3, v4}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    if-eqz p1, :cond_7

    .line 256
    invoke-virtual {p1}, Ljava/util/HashSet;->isEmpty()Z

    move-result v4

    if-nez v4, :cond_7

    .line 257
    invoke-virtual {p1, v3}, Ljava/util/HashSet;->addAll(Ljava/util/Collection;)Z

    goto/16 :goto_1

    .line 260
    :cond_7
    new-instance p1, Ljava/util/HashSet;

    invoke-direct {p1, v3}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    goto/16 :goto_1

    :cond_8
    if-eqz p1, :cond_c

    .line 267
    invoke-virtual {p1}, Ljava/util/HashSet;->isEmpty()Z

    move-result p0

    if-eqz p0, :cond_9

    goto :goto_5

    .line 273
    :cond_9
    invoke-virtual {p1}, Ljava/util/HashSet;->size()I

    move-result p0

    const/4 v2, 0x1

    if-ne p0, v2, :cond_a

    .line 274
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    .line 275
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 276
    invoke-virtual {p1}, Ljava/util/HashSet;->iterator()Ljava/util/Iterator;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    const-string v2, "f"

    .line 277
    invoke-virtual {p0, v2, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "t"

    .line 278
    invoke-virtual {v1, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    invoke-virtual {p0, v2, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 279
    invoke-virtual {v0, p0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    return-object v0

    .line 283
    :cond_a
    new-instance p0, Ljava/util/HashMap;

    invoke-direct {p0}, Ljava/util/HashMap;-><init>()V

    .line 284
    invoke-virtual {p1}, Ljava/util/HashSet;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_4
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_b

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 285
    invoke-virtual {v0, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {p0, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_4

    .line 287
    :cond_b
    new-instance p1, Lcom/helpshift/support/RankComparator;

    invoke-direct {p1, p0}, Lcom/helpshift/support/RankComparator;-><init>(Ljava/util/HashMap;)V

    .line 288
    new-instance v0, Ljava/util/TreeMap;

    invoke-direct {v0, p1}, Ljava/util/TreeMap;-><init>(Ljava/util/Comparator;)V

    .line 289
    invoke-virtual {v0, p0}, Ljava/util/TreeMap;->putAll(Ljava/util/Map;)V

    .line 290
    invoke-static {v0, v1}, Lcom/helpshift/support/HSSearch;->sortMatchedTermsMap(Ljava/util/TreeMap;Ljava/util/HashMap;)Ljava/util/ArrayList;

    move-result-object p0

    return-object p0

    .line 268
    :cond_c
    :goto_5
    new-instance p0, Lcom/helpshift/support/RankComparator;

    invoke-direct {p0, v0}, Lcom/helpshift/support/RankComparator;-><init>(Ljava/util/HashMap;)V

    .line 269
    new-instance p1, Ljava/util/TreeMap;

    invoke-direct {p1, p0}, Ljava/util/TreeMap;-><init>(Ljava/util/Comparator;)V

    .line 270
    invoke-virtual {p1, v0}, Ljava/util/TreeMap;->putAll(Ljava/util/Map;)V

    .line 271
    invoke-static {p1, v1}, Lcom/helpshift/support/HSSearch;->sortMatchedTermsMap(Ljava/util/TreeMap;Ljava/util/HashMap;)Ljava/util/ArrayList;

    move-result-object p0

    return-object p0
.end method

.method public static sanitize(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 118
    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->stripHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/support/HSSearch;->escapeHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 119
    invoke-virtual {p0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/support/util/HSTransliterator;->unidecode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static smallestOf(II)I
    .locals 0

    if-ge p1, p0, :cond_0

    move p0, p1

    :cond_0
    return p0
.end method

.method private static smallestOf(III)I
    .locals 0

    if-ge p1, p0, :cond_0

    move p0, p1

    :cond_0
    if-ge p2, p0, :cond_1

    move p0, p2

    :cond_1
    return p0
.end method

.method private static sortMatchedTermsMap(Ljava/util/TreeMap;Ljava/util/HashMap;)Ljava/util/ArrayList;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/TreeMap;",
            "Ljava/util/HashMap;",
            ")",
            "Ljava/util/ArrayList<",
            "Ljava/util/HashMap;",
            ">;"
        }
    .end annotation

    .line 297
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 298
    invoke-virtual {p0}, Ljava/util/TreeMap;->keySet()Ljava/util/Set;

    move-result-object p0

    .line 299
    invoke-interface {p0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 300
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    const-string v3, "f"

    .line 301
    invoke-virtual {v2, v3, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "t"

    .line 302
    invoke-virtual {p1, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    invoke-virtual {v2, v3, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 303
    invoke-virtual {v0, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method public static stripHtml(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-string v0, "<[^<>]+>"

    const-string v1, ""

    .line 68
    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method
