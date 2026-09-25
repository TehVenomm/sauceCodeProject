.class public Lcom/helpshift/support/search/tfidf/PageIndexTrie;
.super Ljava/lang/Object;
.source "PageIndexTrie.java"


# static fields
.field private static final BATCH_SIZE:I = 0x3e8

.field private static final MAX_TOKEN_SIZE:I = 0x32

.field private static final maxNGramLength:I = 0xa

.field private static final minNGramLength:I = 0x1


# instance fields
.field private root:Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

.field private searchTokenDao:Lcom/helpshift/support/search/SearchTokenDao;

.field private searchTokenList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/search/SearchTokenDto;",
            ">;"
        }
    .end annotation
.end field

.field private final totalDocCount:I


# direct methods
.method public constructor <init>(I)V
    .locals 1

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 29
    iput p1, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->totalDocCount:I

    .line 30
    new-instance p1, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    const/4 v0, 0x0

    invoke-direct {p1, v0}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;-><init>(C)V

    iput-object p1, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->root:Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    .line 31
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    iput-object p1, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenList:Ljava/util/List;

    return-void
.end method

.method private buildTfIdfIndex(Ljava/lang/String;Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;)Lcom/helpshift/support/search/SearchTokenDto;
    .locals 15

    .line 95
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->getMaxFrequency()I

    move-result v0

    .line 96
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->getFaqAppearCount()I

    move-result v1

    .line 98
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 99
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->getWordFrequencyMap()Landroid/util/SparseArray;

    move-result-object v3

    const/4 v4, -0x1

    const/4 v5, 0x0

    .line 101
    :goto_0
    invoke-virtual {v3}, Landroid/util/SparseArray;->size()I

    move-result v6

    if-ge v5, v6, :cond_0

    .line 102
    invoke-virtual {v3, v5}, Landroid/util/SparseArray;->keyAt(I)I

    move-result v6

    .line 103
    invoke-virtual {v3, v5}, Landroid/util/SparseArray;->valueAt(I)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Landroid/util/Pair;

    .line 104
    iget-object v8, v7, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast v8, Ljava/lang/Integer;

    invoke-virtual {v8}, Ljava/lang/Integer;->intValue()I

    move-result v8

    int-to-double v8, v8

    int-to-double v10, v0

    invoke-static {v8, v9}, Ljava/lang/Double;->isNaN(D)Z

    invoke-static {v10, v11}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v8, v10

    move-object v10, p0

    .line 105
    iget v11, v10, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->totalDocCount:I

    int-to-double v11, v11

    int-to-double v13, v1

    invoke-static {v11, v12}, Ljava/lang/Double;->isNaN(D)Z

    invoke-static {v13, v14}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v11, v13

    invoke-static {v11, v12}, Ljava/lang/Math;->log10(D)D

    move-result-wide v11

    mul-double v8, v8, v11

    .line 106
    iget-object v11, v7, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast v11, Ljava/lang/Integer;

    invoke-virtual {v11}, Ljava/lang/Integer;->intValue()I

    move-result v11

    invoke-static {v11}, Lcom/helpshift/support/HSSearch;->getTermWeight(I)I

    move-result v11

    int-to-double v11, v11

    invoke-static {v11, v12}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v8, v8, v11

    .line 107
    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    invoke-static {v8, v9}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v8

    invoke-interface {v2, v6, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 108
    iget-object v6, v7, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast v6, Ljava/lang/Integer;

    invoke-virtual {v6}, Ljava/lang/Integer;->intValue()I

    move-result v6

    invoke-static {v4, v6}, Ljava/lang/Math;->max(II)I

    move-result v4

    add-int/lit8 v5, v5, 0x1

    goto :goto_0

    :cond_0
    move-object v10, p0

    .line 111
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->resetFrequency()V

    .line 112
    new-instance v0, Lcom/helpshift/support/search/SearchTokenDto;

    move-object/from16 v1, p1

    invoke-direct {v0, v1, v4, v2}, Lcom/helpshift/support/search/SearchTokenDto;-><init>(Ljava/lang/String;ILjava/util/Map;)V

    return-object v0
.end method

.method private createAndStoreTfIdfIndex(Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;[CI)V
    .locals 3

    if-nez p1, :cond_0

    return-void

    .line 77
    :cond_0
    iget-char v0, p1, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->nodeValue:C

    aput-char v0, p2, p3

    .line 78
    iget-boolean v0, p1, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->isWordEnd:Z

    if-eqz v0, :cond_1

    .line 79
    new-instance v0, Ljava/lang/String;

    const/4 v1, 0x0

    add-int/lit8 v2, p3, 0x1

    invoke-direct {v0, p2, v1, v2}, Ljava/lang/String;-><init>([CII)V

    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->buildTfIdfIndex(Ljava/lang/String;Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;)Lcom/helpshift/support/search/SearchTokenDto;

    move-result-object v0

    .line 80
    iget-object v1, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenList:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 81
    iget-object v0, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    const/16 v1, 0x3e8

    if-le v0, v1, :cond_1

    .line 82
    iget-object v0, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenDao:Lcom/helpshift/support/search/SearchTokenDao;

    iget-object v1, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenList:Ljava/util/List;

    invoke-interface {v0, v1}, Lcom/helpshift/support/search/SearchTokenDao;->save(Ljava/util/List;)V

    .line 83
    iget-object v0, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->clear()V

    .line 87
    :cond_1
    invoke-virtual {p1}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->getChildren()Ljava/util/List;

    move-result-object v0

    .line 88
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    add-int/lit8 v2, p3, 0x1

    .line 89
    invoke-direct {p0, v1, p2, v2}, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->createAndStoreTfIdfIndex(Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;[CI)V

    goto :goto_0

    .line 91
    :cond_2
    invoke-virtual {p1}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->resetChildren()V

    return-void
.end method


# virtual methods
.method public createAndStoreTfIdfIndex()V
    .locals 4

    .line 63
    invoke-static {}, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->getInstance()Lcom/helpshift/support/search/SearchTokenDao;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenDao:Lcom/helpshift/support/search/SearchTokenDao;

    const/16 v0, 0x32

    .line 64
    new-array v0, v0, [C

    .line 65
    iget-object v1, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->root:Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    invoke-virtual {v1}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->getChildren()Ljava/util/List;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    const/4 v3, 0x0

    .line 66
    invoke-direct {p0, v2, v0, v3}, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->createAndStoreTfIdfIndex(Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;[CI)V

    goto :goto_0

    .line 68
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-lez v0, :cond_1

    .line 69
    iget-object v0, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenDao:Lcom/helpshift/support/search/SearchTokenDao;

    iget-object v1, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->searchTokenList:Ljava/util/List;

    invoke-interface {v0, v1}, Lcom/helpshift/support/search/SearchTokenDao;->save(Ljava/util/List;)V

    :cond_1
    return-void
.end method

.method public insert(Ljava/lang/String;II)V
    .locals 7

    if-eqz p1, :cond_4

    .line 35
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    const/16 v1, 0x32

    if-ge v1, v0, :cond_0

    goto :goto_1

    .line 39
    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    .line 40
    iget-object v2, p0, Lcom/helpshift/support/search/tfidf/PageIndexTrie;->root:Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    const/4 v3, 0x0

    :goto_0
    const/4 v4, 0x1

    if-ge v3, v0, :cond_3

    .line 43
    invoke-virtual {p1, v3}, Ljava/lang/String;->charAt(I)C

    move-result v5

    .line 44
    invoke-virtual {v2, v5}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->getChild(C)Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    move-result-object v6

    if-nez v6, :cond_1

    .line 46
    new-instance v6, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;

    invoke-direct {v6, v5}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;-><init>(C)V

    .line 47
    invoke-virtual {v2, v6}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->addChild(Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;)V

    :cond_1
    move-object v2, v6

    if-eq p2, v1, :cond_2

    if-le v3, v4, :cond_2

    const/16 v5, 0xa

    if-ge v3, v5, :cond_2

    add-int/lit8 v5, v3, 0x1

    if-eq v5, v0, :cond_2

    .line 51
    iput-boolean v4, v2, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->isWordEnd:Z

    .line 52
    invoke-static {v3, p2}, Lcom/helpshift/support/HSSearch;->calcFreq(II)I

    move-result v4

    mul-int v4, v4, v3

    div-int/2addr v4, v0

    .line 53
    invoke-virtual {v2, p3, v4, p2}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->addFrequency(III)V

    :cond_2
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 57
    :cond_3
    iput-boolean v4, v2, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->isWordEnd:Z

    .line 58
    invoke-static {v0, p2}, Lcom/helpshift/support/HSSearch;->calcFreq(II)I

    move-result p1

    .line 59
    invoke-virtual {v2, p3, p1, p2}, Lcom/helpshift/support/search/tfidf/PageIndexTrieNode;->addFrequency(III)V

    return-void

    :cond_4
    :goto_1
    return-void
.end method
