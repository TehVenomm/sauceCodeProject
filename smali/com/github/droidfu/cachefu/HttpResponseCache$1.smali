.class Lcom/github/droidfu/cachefu/HttpResponseCache$1;
.super Ljava/lang/Object;
.source "HttpResponseCache.java"

# interfaces
.implements Ljava/io/FilenameFilter;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/github/droidfu/cachefu/HttpResponseCache;->removeExpiredCache(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/github/droidfu/cachefu/HttpResponseCache;

.field final synthetic val$cacheDir:Ljava/io/File;

.field final synthetic val$urlPrefix:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/github/droidfu/cachefu/HttpResponseCache;Ljava/io/File;Ljava/lang/String;)V
    .locals 0

    .line 49
    iput-object p1, p0, Lcom/github/droidfu/cachefu/HttpResponseCache$1;->this$0:Lcom/github/droidfu/cachefu/HttpResponseCache;

    iput-object p2, p0, Lcom/github/droidfu/cachefu/HttpResponseCache$1;->val$cacheDir:Ljava/io/File;

    iput-object p3, p0, Lcom/github/droidfu/cachefu/HttpResponseCache$1;->val$urlPrefix:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public accept(Ljava/io/File;Ljava/lang/String;)Z
    .locals 1

    .line 52
    iget-object v0, p0, Lcom/github/droidfu/cachefu/HttpResponseCache$1;->val$cacheDir:Ljava/io/File;

    invoke-virtual {p1, v0}, Ljava/io/File;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/github/droidfu/cachefu/HttpResponseCache$1;->this$0:Lcom/github/droidfu/cachefu/HttpResponseCache;

    iget-object v0, p0, Lcom/github/droidfu/cachefu/HttpResponseCache$1;->val$urlPrefix:Ljava/lang/String;

    invoke-virtual {p1, v0}, Lcom/github/droidfu/cachefu/HttpResponseCache;->getFileNameForKey(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method
