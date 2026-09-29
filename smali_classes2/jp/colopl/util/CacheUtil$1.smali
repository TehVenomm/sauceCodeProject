.class final Ljp/colopl/util/CacheUtil$1;
.super Ljava/lang/Object;
.source "CacheUtil.java"

# interfaces
.implements Ljava/io/FilenameFilter;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/util/CacheUtil;->removeExpiredCache(Lcom/github/droidfu/cachefu/AbstractCache;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$cache:Lcom/github/droidfu/cachefu/AbstractCache;

.field final synthetic val$cacheDir:Ljava/io/File;

.field final synthetic val$urlPrefix:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/io/File;Lcom/github/droidfu/cachefu/AbstractCache;Ljava/lang/String;)V
    .locals 0

    .line 101
    iput-object p1, p0, Ljp/colopl/util/CacheUtil$1;->val$cacheDir:Ljava/io/File;

    iput-object p2, p0, Ljp/colopl/util/CacheUtil$1;->val$cache:Lcom/github/droidfu/cachefu/AbstractCache;

    iput-object p3, p0, Ljp/colopl/util/CacheUtil$1;->val$urlPrefix:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public accept(Ljava/io/File;Ljava/lang/String;)Z
    .locals 1

    .line 104
    iget-object v0, p0, Ljp/colopl/util/CacheUtil$1;->val$cacheDir:Ljava/io/File;

    invoke-virtual {p1, v0}, Ljava/io/File;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Ljp/colopl/util/CacheUtil$1;->val$cache:Lcom/github/droidfu/cachefu/AbstractCache;

    iget-object v0, p0, Ljp/colopl/util/CacheUtil$1;->val$urlPrefix:Ljava/lang/String;

    .line 105
    invoke-virtual {p1, v0}, Lcom/github/droidfu/cachefu/AbstractCache;->getFileNameForKey(Ljava/lang/Object;)Ljava/lang/String;

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
