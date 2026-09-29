.class public Lcom/helpshift/util/VersionName;
.super Ljava/lang/Object;
.source "VersionName.java"

# interfaces
.implements Ljava/lang/Comparable;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/lang/Comparable<",
        "Lcom/helpshift/util/VersionName;",
        ">;"
    }
.end annotation


# instance fields
.field public final numbers:[I
    .annotation build Landroidx/annotation/NonNull;
    .end annotation
.end field


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 3
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "-"

    .line 18
    invoke-virtual {p1, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    const/4 v0, 0x0

    aget-object p1, p1, v0

    const-string v1, "\\."

    invoke-virtual {p1, v1}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    .line 19
    array-length v1, p1

    new-array v1, v1, [I

    iput-object v1, p0, Lcom/helpshift/util/VersionName;->numbers:[I

    .line 20
    :goto_0
    array-length v1, p1

    if-ge v0, v1, :cond_0

    .line 21
    iget-object v1, p0, Lcom/helpshift/util/VersionName;->numbers:[I

    aget-object v2, p1, v0

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Integer;->intValue()I

    move-result v2

    aput v2, v1, v0

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method


# virtual methods
.method public compareTo(Lcom/helpshift/util/VersionName;)I
    .locals 5
    .param p1    # Lcom/helpshift/util/VersionName;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 27
    iget-object v0, p0, Lcom/helpshift/util/VersionName;->numbers:[I

    array-length v0, v0

    iget-object v1, p1, Lcom/helpshift/util/VersionName;->numbers:[I

    array-length v1, v1

    invoke-static {v0, v1}, Ljava/lang/Math;->max(II)I

    move-result v0

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_4

    .line 29
    iget-object v3, p0, Lcom/helpshift/util/VersionName;->numbers:[I

    array-length v3, v3

    if-ge v2, v3, :cond_0

    iget-object v3, p0, Lcom/helpshift/util/VersionName;->numbers:[I

    aget v3, v3, v2

    goto :goto_1

    :cond_0
    const/4 v3, 0x0

    .line 30
    :goto_1
    iget-object v4, p1, Lcom/helpshift/util/VersionName;->numbers:[I

    array-length v4, v4

    if-ge v2, v4, :cond_1

    iget-object v4, p1, Lcom/helpshift/util/VersionName;->numbers:[I

    aget v4, v4, v2

    goto :goto_2

    :cond_1
    const/4 v4, 0x0

    :goto_2
    if-eq v3, v4, :cond_3

    if-ge v3, v4, :cond_2

    const/4 p1, -0x1

    goto :goto_3

    :cond_2
    const/4 p1, 0x1

    :goto_3
    return p1

    :cond_3
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_4
    return v1
.end method

.method public bridge synthetic compareTo(Ljava/lang/Object;)I
    .locals 0
    .param p1    # Ljava/lang/Object;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 12
    check-cast p1, Lcom/helpshift/util/VersionName;

    invoke-virtual {p0, p1}, Lcom/helpshift/util/VersionName;->compareTo(Lcom/helpshift/util/VersionName;)I

    move-result p1

    return p1
.end method

.method public isGreaterThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z
    .locals 1

    .line 39
    invoke-virtual {p0, p1}, Lcom/helpshift/util/VersionName;->compareTo(Lcom/helpshift/util/VersionName;)I

    move-result p1

    const/4 v0, 0x1

    if-eq p1, v0, :cond_1

    if-nez p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :cond_1
    :goto_0
    return v0
.end method

.method public isLessThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z
    .locals 1

    .line 44
    invoke-virtual {p0, p1}, Lcom/helpshift/util/VersionName;->compareTo(Lcom/helpshift/util/VersionName;)I

    move-result p1

    const/4 v0, -0x1

    if-eq p1, v0, :cond_1

    if-nez p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method
