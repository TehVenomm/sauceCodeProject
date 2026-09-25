.class public Lcom/helpshift/support/storage/FaqsDataSource;
.super Ljava/lang/Object;
.source "FaqsDataSource.java"

# interfaces
.implements Lcom/helpshift/support/storage/FaqDAO;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/storage/FaqsDataSource$LazyHolder;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "HelpShiftDebug"


# instance fields
.field private final dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;


# direct methods
.method private constructor <init>()V
    .locals 1

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    invoke-static {}, Lcom/helpshift/support/storage/FaqsDBHelper;->getInstance()Lcom/helpshift/support/storage/FaqsDBHelper;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    return-void
.end method

.method synthetic constructor <init>(Lcom/helpshift/support/storage/FaqsDataSource$1;)V
    .locals 0

    .line 23
    invoke-direct {p0}, Lcom/helpshift/support/storage/FaqsDataSource;-><init>()V

    return-void
.end method

.method public static addFaqsUnsafe(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;Lorg/json/JSONArray;)V
    .locals 4

    const/4 v0, 0x0

    .line 37
    :goto_0
    :try_start_0
    invoke-virtual {p2}, Lorg/json/JSONArray;->length()I

    move-result v1

    if-ge v0, v1, :cond_0

    .line 38
    invoke-virtual {p2, v0}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v1

    const-string v2, "faqs"

    const/4 v3, 0x0

    .line 39
    invoke-static {p1, v1}, Lcom/helpshift/support/storage/FaqsDataSource;->faqToContentValues(Ljava/lang/String;Lorg/json/JSONObject;)Landroid/content/ContentValues;

    move-result-object v1

    invoke-virtual {p0, v2, v3, v1}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :catch_0
    move-exception p0

    const-string p1, "HelpShiftDebug"

    const-string p2, "addFaqsUnsafe"

    .line 43
    invoke-static {p1, p2, p0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    return-void
.end method

.method private static cursorToFaq(Landroid/database/Cursor;)Lcom/helpshift/support/Faq;
    .locals 14

    .line 48
    new-instance v13, Lcom/helpshift/support/Faq;

    const-string v0, "_id"

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v1

    const-string v0, "question_id"

    .line 49
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    const-string v0, "publish_id"

    .line 50
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    const-string v0, "language"

    .line 51
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v5

    const-string v0, "section_id"

    .line 52
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v6

    const-string v0, "title"

    .line 53
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v7

    const-string v0, "body"

    .line 54
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v8

    const-string v0, "helpful"

    .line 55
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v9

    const-string v0, "rtl"

    .line 56
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    const/4 v10, 0x1

    if-ne v0, v10, :cond_0

    goto :goto_0

    :cond_0
    const/4 v10, 0x0

    :goto_0
    invoke-static {v10}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v10

    const-string v0, "tags"

    .line 57
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/util/HSJSONUtils;->jsonToStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v11

    const-string v0, "c_tags"

    .line 59
    invoke-interface {p0, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p0, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/util/HSJSONUtils;->jsonToStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v12

    move-object v0, v13

    invoke-direct/range {v0 .. v12}, Lcom/helpshift/support/Faq;-><init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/Boolean;Ljava/util/List;Ljava/util/List;)V

    return-object v13
.end method

.method private static faqToContentValues(Lcom/helpshift/support/Faq;)Landroid/content/ContentValues;
    .locals 4

    .line 63
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "question_id"

    .line 64
    invoke-virtual {p0}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "publish_id"

    .line 65
    iget-object v2, p0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "language"

    .line 66
    iget-object v2, p0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "section_id"

    .line 67
    iget-object v2, p0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "title"

    .line 68
    iget-object v2, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "body"

    .line 69
    iget-object v2, p0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "helpful"

    .line 70
    iget v2, p0, Lcom/helpshift/support/Faq;->is_helpful:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v1, "rtl"

    .line 71
    iget-object v2, p0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    const-string v1, "tags"

    .line 72
    new-instance v2, Lorg/json/JSONArray;

    invoke-virtual {p0}, Lcom/helpshift/support/Faq;->getTags()Ljava/util/List;

    move-result-object v3

    invoke-direct {v2, v3}, Lorg/json/JSONArray;-><init>(Ljava/util/Collection;)V

    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "c_tags"

    .line 73
    new-instance v2, Lorg/json/JSONArray;

    invoke-virtual {p0}, Lcom/helpshift/support/Faq;->getCategoryTags()Ljava/util/List;

    move-result-object p0

    invoke-direct {v2, p0}, Lorg/json/JSONArray;-><init>(Ljava/util/Collection;)V

    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, v1, p0}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method

.method private static faqToContentValues(Ljava/lang/String;Lorg/json/JSONObject;)Landroid/content/ContentValues;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 79
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v1, "question_id"

    const-string v2, "id"

    .line 80
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "publish_id"

    const-string v2, "publish_id"

    .line 81
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "language"

    const-string v2, "language"

    .line 82
    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "section_id"

    .line 83
    invoke-virtual {v0, v1, p0}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "title"

    const-string v1, "title"

    .line 84
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p0, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "body"

    const-string v1, "body"

    .line 85
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p0, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "helpful"

    const/4 v1, 0x0

    .line 86
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v0, p0, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string p0, "rtl"

    const-string v1, "is_rtl"

    .line 87
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "true"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {v0, p0, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Boolean;)V

    const-string p0, "tags"

    const-string v1, "stags"

    .line 88
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v1, "stags"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1

    :goto_0
    invoke-virtual {v1}, Lorg/json/JSONArray;->toString()Ljava/lang/String;

    move-result-object v1

    goto :goto_1

    :cond_0
    new-instance v1, Lorg/json/JSONArray;

    invoke-direct {v1}, Lorg/json/JSONArray;-><init>()V

    goto :goto_0

    :goto_1
    invoke-virtual {v0, p0, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "c_tags"

    const-string v1, "issue_tags"

    .line 90
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    const-string v1, "issue_tags"

    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p1

    :goto_2
    invoke-virtual {p1}, Lorg/json/JSONArray;->toString()Ljava/lang/String;

    move-result-object p1

    goto :goto_3

    :cond_1
    new-instance p1, Lorg/json/JSONArray;

    invoke-direct {p1}, Lorg/json/JSONArray;-><init>()V

    goto :goto_2

    .line 89
    :goto_3
    invoke-virtual {v0, p0, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method

.method private getANDFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation

    .line 328
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 330
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/Faq;

    .line 331
    new-instance v2, Ljava/util/ArrayList;

    invoke-virtual {p2}, Lcom/helpshift/support/FaqTagFilter;->getTags()[Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 332
    invoke-virtual {v1}, Lcom/helpshift/support/Faq;->getCategoryTags()Ljava/util/List;

    move-result-object v3

    .line 333
    invoke-interface {v2, v3}, Ljava/util/List;->removeAll(Ljava/util/Collection;)Z

    .line 334
    invoke-interface {v2}, Ljava/util/List;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_0

    .line 335
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method public static getInstance()Lcom/helpshift/support/storage/FaqsDataSource;
    .locals 1

    .line 95
    sget-object v0, Lcom/helpshift/support/storage/FaqsDataSource$LazyHolder;->INSTANCE:Lcom/helpshift/support/storage/FaqsDataSource;

    return-object v0
.end method

.method private getNOTFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation

    .line 355
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 357
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/Faq;

    .line 358
    new-instance v2, Ljava/util/ArrayList;

    invoke-virtual {p2}, Lcom/helpshift/support/FaqTagFilter;->getTags()[Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 359
    invoke-virtual {v1}, Lcom/helpshift/support/Faq;->getCategoryTags()Ljava/util/List;

    move-result-object v3

    .line 360
    invoke-interface {v2, v3}, Ljava/util/List;->removeAll(Ljava/util/Collection;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 361
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method private getORFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation

    .line 342
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 344
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/Faq;

    .line 345
    new-instance v2, Ljava/util/ArrayList;

    invoke-virtual {p2}, Lcom/helpshift/support/FaqTagFilter;->getTags()[Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 346
    invoke-virtual {v1}, Lcom/helpshift/support/Faq;->getCategoryTags()Ljava/util/List;

    move-result-object v3

    .line 347
    invoke-interface {v2, v3}, Ljava/util/List;->removeAll(Ljava/util/Collection;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 348
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object v0
.end method


# virtual methods
.method public declared-synchronized addFaq(Lcom/helpshift/support/Faq;)V
    .locals 4

    monitor-enter p0

    .line 111
    :try_start_0
    invoke-static {p1}, Lcom/helpshift/support/storage/FaqsDataSource;->faqToContentValues(Lcom/helpshift/support/Faq;)Landroid/content/ContentValues;

    move-result-object v0

    const-string v1, "question_id=?"

    const/4 v2, 0x1

    .line 113
    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    invoke-virtual {p1}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v2, v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 115
    :try_start_1
    iget-object p1, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {p1}, Lcom/helpshift/support/storage/FaqsDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p1

    const-string v3, "faqs"

    .line 116
    invoke-static {p1, v3, v1, v2}, Lcom/helpshift/util/DatabaseUtils;->exists(Landroid/database/sqlite/SQLiteDatabase;Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_0

    const-string v1, "faqs"

    const/4 v2, 0x0

    .line 120
    invoke-virtual {p1, v1, v2, v0}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    goto :goto_0

    :cond_0
    const-string v3, "faqs"

    .line 123
    invoke-virtual {p1, v3, v0, v1, v2}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string v0, "HelpShiftDebug"

    const-string v1, "Error in addFaq"

    .line 129
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 131
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 110
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized clearDB()V
    .locals 3

    monitor-enter p0

    .line 100
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/support/storage/FaqsDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    .line 101
    iget-object v1, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {v1, v0}, Lcom/helpshift/support/storage/FaqsDBHelper;->clearDatabase(Landroid/database/sqlite/SQLiteDatabase;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception v0

    goto :goto_1

    :catch_0
    move-exception v0

    :try_start_1
    const-string v1, "HelpShiftDebug"

    const-string v2, "Error in clearDB"

    .line 104
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 107
    :goto_0
    monitor-exit p0

    return-void

    .line 99
    :goto_1
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getAllFaqPublishIds()Ljava/util/List;
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 302
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v1, 0x0

    :try_start_1
    const-string v2, "publish_id"

    .line 305
    filled-new-array {v2}, [Ljava/lang/String;

    move-result-object v5

    .line 306
    iget-object v2, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/support/storage/FaqsDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    const-string v4, "faqs"

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    .line 307
    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 308
    :try_start_2
    invoke-interface {v2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 309
    :goto_0
    invoke-interface {v2}, Landroid/database/Cursor;->isAfterLast()Z

    move-result v1

    if-nez v1, :cond_0

    const-string v1, "publish_id"

    .line 310
    invoke-interface {v2, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {v2, v1}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v1

    .line 311
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 312
    invoke-interface {v2}, Landroid/database/Cursor;->moveToNext()Z
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    goto :goto_0

    :cond_0
    if-eqz v2, :cond_1

    .line 321
    :goto_1
    :try_start_3
    invoke-interface {v2}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_3

    :catch_0
    move-exception v1

    goto :goto_2

    :catchall_0
    move-exception v0

    move-object v2, v1

    goto :goto_4

    :catch_1
    move-exception v2

    move-object v11, v2

    move-object v2, v1

    move-object v1, v11

    :goto_2
    :try_start_4
    const-string v3, "HelpShiftDebug"

    const-string v4, "Error in getFaqsDataForSection"

    .line 317
    invoke-static {v3, v4, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v2, :cond_1

    goto :goto_1

    .line 324
    :cond_1
    :goto_3
    monitor-exit p0

    return-object v0

    :catchall_1
    move-exception v0

    :goto_4
    if-eqz v2, :cond_2

    .line 321
    :try_start_5
    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    .line 323
    :cond_2
    throw v0
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception v0

    .line 301
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getFaq(Ljava/lang/String;)Lcom/helpshift/support/Faq;
    .locals 10

    monitor-enter p0

    .line 150
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 151
    monitor-exit p0

    return-object v1

    .line 158
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/support/storage/FaqsDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    const-string v3, "faqs"

    const/4 v4, 0x0

    const-string v5, "publish_id = ?"

    const/4 v0, 0x1

    .line 159
    new-array v6, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    aput-object p1, v6, v0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 163
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 164
    invoke-static {p1}, Lcom/helpshift/support/storage/FaqsDataSource;->cursorToFaq(Landroid/database/Cursor;)Lcom/helpshift/support/Faq;

    move-result-object v0
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object v1, v0

    :cond_1
    if-eqz p1, :cond_2

    .line 172
    :goto_0
    :try_start_3
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception v0

    goto :goto_1

    :catchall_0
    move-exception v0

    goto :goto_3

    :catch_1
    move-exception v0

    move-object p1, v1

    :goto_1
    :try_start_4
    const-string v2, "HelpShiftDebug"

    const-string v3, "Error in getFaq"

    .line 168
    invoke-static {v2, v3, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz p1, :cond_2

    goto :goto_0

    .line 176
    :cond_2
    :goto_2
    monitor-exit p0

    return-object v1

    :catchall_1
    move-exception v0

    move-object v1, p1

    :goto_3
    if-eqz v1, :cond_3

    .line 172
    :try_start_5
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 174
    :cond_3
    throw v0
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    :catchall_2
    move-exception p1

    .line 149
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getFaq(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/support/Faq;
    .locals 10

    monitor-enter p0

    .line 181
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_4

    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    if-eqz v0, :cond_0

    goto :goto_4

    .line 189
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {v0}, Lcom/helpshift/support/storage/FaqsDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    const-string v3, "faqs"

    const/4 v4, 0x0

    const-string v5, "publish_id = ? AND language = ?"

    const/4 v0, 0x2

    .line 190
    new-array v6, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    aput-object p1, v6, v0

    const/4 p1, 0x1

    aput-object p2, v6, p1

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 196
    :try_start_2
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_1

    .line 197
    invoke-static {p1}, Lcom/helpshift/support/storage/FaqsDataSource;->cursorToFaq(Landroid/database/Cursor;)Lcom/helpshift/support/Faq;

    move-result-object p2
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    move-object v1, p2

    :cond_1
    if-eqz p1, :cond_2

    .line 205
    :goto_0
    :try_start_3
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_2

    goto :goto_2

    :catch_0
    move-exception p2

    goto :goto_1

    :catchall_0
    move-exception p2

    goto :goto_3

    :catch_1
    move-exception p2

    move-object p1, v1

    :goto_1
    :try_start_4
    const-string v0, "HelpShiftDebug"

    const-string v2, "Error in getFaq"

    .line 201
    invoke-static {v0, v2, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz p1, :cond_2

    goto :goto_0

    .line 209
    :cond_2
    :goto_2
    monitor-exit p0

    return-object v1

    :catchall_1
    move-exception p2

    move-object v1, p1

    :goto_3
    if-eqz v1, :cond_3

    .line 205
    :try_start_5
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 207
    :cond_3
    throw p2
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    .line 182
    :cond_4
    :goto_4
    monitor-exit p0

    return-object v1

    :catchall_2
    move-exception p1

    .line 180
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getFaqsDataForSection(Ljava/lang/String;)Ljava/util/List;
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 262
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 263
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    monitor-exit p0

    return-object p1

    .line 266
    :cond_0
    :try_start_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    const/4 v1, 0x0

    .line 270
    :try_start_2
    iget-object v2, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {v2}, Lcom/helpshift/support/storage/FaqsDBHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v3

    const-string v4, "faqs"

    const/4 v5, 0x0

    const-string v6, "section_id = ?"

    const/4 v2, 0x1

    .line 271
    new-array v7, v2, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object p1, v7, v2

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    invoke-virtual/range {v3 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 275
    :try_start_3
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 276
    :goto_0
    invoke-interface {p1}, Landroid/database/Cursor;->isAfterLast()Z

    move-result v1

    if-nez v1, :cond_1

    .line 277
    invoke-static {p1}, Lcom/helpshift/support/storage/FaqsDataSource;->cursorToFaq(Landroid/database/Cursor;)Lcom/helpshift/support/Faq;

    move-result-object v1

    .line 278
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 279
    invoke-interface {p1}, Landroid/database/Cursor;->moveToNext()Z
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    :cond_1
    if-eqz p1, :cond_2

    .line 288
    :try_start_4
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    goto :goto_2

    :catchall_0
    move-exception v0

    move-object v1, p1

    goto :goto_3

    :catch_0
    move-exception v1

    move-object v11, v1

    move-object v1, p1

    move-object p1, v11

    goto :goto_1

    :catchall_1
    move-exception v0

    goto :goto_3

    :catch_1
    move-exception p1

    :goto_1
    :try_start_5
    const-string v2, "HelpShiftDebug"

    const-string v3, "Error in getFaqsDataForSection"

    .line 284
    invoke-static {v2, v3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    if-eqz v1, :cond_2

    .line 288
    :try_start_6
    invoke-interface {v1}, Landroid/database/Cursor;->close()V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    .line 292
    :cond_2
    :goto_2
    monitor-exit p0

    return-object v0

    :goto_3
    if-eqz v1, :cond_3

    .line 288
    :try_start_7
    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    .line 290
    :cond_3
    throw v0
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    :catchall_2
    move-exception p1

    .line 261
    monitor-exit p0

    throw p1
.end method

.method public getFaqsForSection(Ljava/lang/String;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation

    .line 232
    invoke-virtual {p0, p1}, Lcom/helpshift/support/storage/FaqsDataSource;->getFaqsDataForSection(Ljava/lang/String;)Ljava/util/List;

    move-result-object p1

    .line 233
    invoke-virtual {p0, p1, p2}, Lcom/helpshift/support/storage/FaqsDataSource;->getFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;

    move-result-object p1

    return-object p1
.end method

.method public getFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation

    if-nez p2, :cond_0

    return-object p1

    .line 217
    :cond_0
    invoke-virtual {p2}, Lcom/helpshift/support/FaqTagFilter;->getOperator()Ljava/lang/String;

    move-result-object v0

    const/4 v1, -0x1

    invoke-virtual {v0}, Ljava/lang/String;->hashCode()I

    move-result v2

    const v3, -0x3de09eb0

    if-eq v2, v3, :cond_4

    const/16 v3, 0xde3

    if-eq v2, v3, :cond_3

    const v3, 0x179d7

    if-eq v2, v3, :cond_2

    const v3, 0x1aad3

    if-eq v2, v3, :cond_1

    goto :goto_0

    :cond_1
    const-string v2, "not"

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_5

    const/4 v1, 0x2

    goto :goto_0

    :cond_2
    const-string v2, "and"

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_5

    const/4 v1, 0x0

    goto :goto_0

    :cond_3
    const-string v2, "or"

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_5

    const/4 v1, 0x1

    goto :goto_0

    :cond_4
    const-string v2, "undefined"

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_5

    const/4 v1, 0x3

    :cond_5
    :goto_0
    packed-switch v1, :pswitch_data_0

    return-object p1

    :pswitch_0
    return-object p1

    .line 223
    :pswitch_1
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/storage/FaqsDataSource;->getNOTFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;

    move-result-object p1

    return-object p1

    .line 221
    :pswitch_2
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/storage/FaqsDataSource;->getORFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;

    move-result-object p1

    return-object p1

    .line 219
    :pswitch_3
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/storage/FaqsDataSource;->getANDFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;

    move-result-object p1

    return-object p1

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public declared-synchronized removeFaq(Ljava/lang/String;)V
    .locals 3

    monitor-enter p0

    .line 135
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    :try_start_1
    const-string v0, "publish_id=?"

    const/4 v1, 0x1

    .line 138
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object p1, v1, v2

    .line 139
    iget-object p1, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {p1}, Lcom/helpshift/support/storage/FaqsDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p1

    const-string v2, "faqs"

    .line 140
    invoke-virtual {p1, v2, v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    :try_start_2
    const-string v0, "HelpShiftDebug"

    const-string v1, "Error in removeFaq"

    .line 143
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 146
    :cond_0
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 134
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized setIsHelpful(Ljava/lang/String;Ljava/lang/Boolean;)I
    .locals 5

    monitor-enter p0

    .line 239
    :try_start_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 240
    monitor-exit p0

    return v1

    .line 243
    :cond_0
    :try_start_1
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v2, "helpful"

    .line 244
    invoke-virtual {p2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p2

    const/4 v3, 0x1

    if-eqz p2, :cond_1

    const/4 p2, 0x1

    goto :goto_0

    :cond_1
    const/4 p2, -0x1

    :goto_0
    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {v0, v2, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 247
    :try_start_2
    iget-object p2, p0, Lcom/helpshift/support/storage/FaqsDataSource;->dbHelper:Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-virtual {p2}, Lcom/helpshift/support/storage/FaqsDBHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p2

    const-string v2, "faqs"

    const-string v4, "question_id = ?"

    .line 248
    new-array v3, v3, [Ljava/lang/String;

    aput-object p1, v3, v1

    invoke-virtual {p2, v2, v0, v4, v3}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    move-result p1
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_3
    const-string p2, "HelpShiftDebug"

    const-string v0, "Error in setIsHelpful"

    .line 254
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    const/4 p1, 0x0

    .line 257
    :goto_1
    monitor-exit p0

    return p1

    :catchall_0
    move-exception p1

    .line 237
    monitor-exit p0

    throw p1
.end method
