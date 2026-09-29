.class public Lcom/zopim/android/sdk/model/Attachment;
.super Ljava/lang/Object;


# annotations
.annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnoreProperties;
    ignoreUnknown = true
.end annotation


# static fields
.field private static final LOG_TAG:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnore;
    .end annotation
.end field


# instance fields
.field private mimeType:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "mime_type$string"
    .end annotation
.end field

.field private name:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "name$string"
    .end annotation
.end field

.field private size:Ljava/lang/Long;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "size$int"
    .end annotation
.end field

.field private thumbnail:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "thumb$string"
    .end annotation
.end field

.field private type:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "type$string"
    .end annotation
.end field

.field private url:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "url$string"
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/model/Attachment;->LOG_TAG:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getMimeType()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Attachment;->mimeType:Ljava/lang/String;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Attachment;->name:Ljava/lang/String;

    return-object v0
.end method

.method public getSize()Ljava/lang/Long;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Attachment;->size:Ljava/lang/Long;

    return-object v0
.end method

.method public getThumbnail()Ljava/net/URL;
    .locals 4
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Attachment;->thumbnail:Ljava/lang/String;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    :try_start_0
    new-instance v0, Ljava/net/URL;

    iget-object v2, p0, Lcom/zopim/android/sdk/model/Attachment;->thumbnail:Ljava/lang/String;

    invoke-direct {v0, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/model/Attachment;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Can not retrieve url. "

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    return-object v1
.end method

.method public getType()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Attachment;->type:Ljava/lang/String;

    return-object v0
.end method

.method public getUrl()Ljava/net/URL;
    .locals 4
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Attachment;->url:Ljava/lang/String;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    :try_start_0
    new-instance v0, Ljava/net/URL;

    iget-object v2, p0, Lcom/zopim/android/sdk/model/Attachment;->url:Ljava/lang/String;

    invoke-direct {v0, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/model/Attachment;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Can not retrieve url. "

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    return-object v1
.end method
