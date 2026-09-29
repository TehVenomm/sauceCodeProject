.class public Lcom/github/droidfu/cachefu/CacheHelper;
.super Ljava/lang/Object;
.source "CacheHelper.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getFileNameFromUrl(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    const-string v0, "[.:/,%?&=]"

    const-string v1, "+"

    .line 7
    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    const-string v0, "[+]+"

    const-string v1, "+"

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method
