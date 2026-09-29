.class public final Lnet/gogame/gowrap/support/FaqSupport;
.super Ljava/lang/Object;
.source "FaqSupport.java"


# static fields
.field private static final KEY_FAQS:Ljava/lang/String; = "faqs"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getFaq(Landroid/content/Context;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;
    .locals 2

    .line 29
    :try_start_0
    new-instance v0, Ljava/io/File;

    invoke-virtual {p0}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object p0

    const-string v1, "net/gogame/gowrap/faq.json.gz"

    invoke-direct {v0, p0, v1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    invoke-static {v0, p1}, Lnet/gogame/gowrap/support/FaqSupport;->parse(Ljava/io/File;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    const-string p1, "goWrap"

    const-string v0, "Error loading FAQ"

    .line 32
    invoke-static {p1, v0, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    const/4 p0, 0x0

    return-object p0
.end method

.method private static parse(Landroid/util/JsonReader;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 57
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_OBJECT:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_2

    const/4 v0, 0x0

    .line 59
    invoke-virtual {p0}, Landroid/util/JsonReader;->beginObject()V

    .line 60
    :goto_0
    invoke-virtual {p0}, Landroid/util/JsonReader;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 61
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextName()Ljava/lang/String;

    move-result-object v1

    const-string v2, "faqs"

    .line 62
    invoke-static {v1, v2}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 63
    invoke-static {p0, p1}, Lnet/gogame/gowrap/support/FaqSupport;->parseFaqs(Landroid/util/JsonReader;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;

    move-result-object v0

    goto :goto_0

    .line 65
    :cond_0
    invoke-virtual {p0}, Landroid/util/JsonReader;->skipValue()V

    goto :goto_0

    .line 68
    :cond_1
    invoke-virtual {p0}, Landroid/util/JsonReader;->endObject()V

    return-object v0

    .line 71
    :cond_2
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string p1, "object expected"

    invoke-direct {p0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method

.method private static parse(Ljava/io/File;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 38
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->newInputStream(Ljava/io/File;)Ljava/io/InputStream;

    move-result-object p0

    .line 40
    :try_start_0
    new-instance v0, Ljava/io/InputStreamReader;

    const-string v1, "UTF-8"

    invoke-direct {v0, p0, v1}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 42
    :try_start_1
    new-instance v1, Landroid/util/JsonReader;

    invoke-direct {v1, v0}, Landroid/util/JsonReader;-><init>(Ljava/io/Reader;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 44
    :try_start_2
    invoke-static {v1, p1}, Lnet/gogame/gowrap/support/FaqSupport;->parse(Landroid/util/JsonReader;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;

    move-result-object p1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 46
    :try_start_3
    invoke-static {v1}, Lnet/gogame/gowrap/support/JSONUtils;->closeQuietly(Landroid/util/JsonReader;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    .line 49
    :try_start_4
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/Reader;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    .line 52
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-object p1

    :catchall_0
    move-exception p1

    .line 46
    :try_start_5
    invoke-static {v1}, Lnet/gogame/gowrap/support/JSONUtils;->closeQuietly(Landroid/util/JsonReader;)V

    .line 47
    throw p1
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    :catchall_1
    move-exception p1

    .line 49
    :try_start_6
    invoke-static {v0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/Reader;)V

    .line 50
    throw p1
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    :catchall_2
    move-exception p1

    .line 52
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 53
    throw p1
.end method

.method private static parseFaqs(Landroid/util/JsonReader;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 76
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->BEGIN_OBJECT:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_5

    .line 79
    invoke-virtual {p0}, Landroid/util/JsonReader;->beginObject()V

    const/4 v0, 0x0

    move-object v1, v0

    .line 80
    :goto_0
    invoke-virtual {p0}, Landroid/util/JsonReader;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 81
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextName()Ljava/lang/String;

    move-result-object v2

    if-nez v0, :cond_2

    .line 83
    invoke-static {v2, p1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 84
    new-instance v0, Lnet/gogame/gowrap/model/faq/Category;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/model/faq/Category;-><init>(Landroid/util/JsonReader;)V

    goto :goto_0

    :cond_0
    const-string v3, "default"

    .line 85
    invoke-static {v2, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 86
    new-instance v1, Lnet/gogame/gowrap/model/faq/Category;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/model/faq/Category;-><init>(Landroid/util/JsonReader;)V

    goto :goto_0

    .line 88
    :cond_1
    invoke-virtual {p0}, Landroid/util/JsonReader;->skipValue()V

    goto :goto_0

    .line 91
    :cond_2
    invoke-virtual {p0}, Landroid/util/JsonReader;->skipValue()V

    goto :goto_0

    .line 94
    :cond_3
    invoke-virtual {p0}, Landroid/util/JsonReader;->endObject()V

    if-eqz v0, :cond_4

    return-object v0

    :cond_4
    return-object v1

    .line 101
    :cond_5
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string p1, "object expected"

    invoke-direct {p0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method
