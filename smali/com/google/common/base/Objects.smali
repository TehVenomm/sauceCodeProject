.class public final Lcom/google/common/base/Objects;
.super Ljava/lang/Object;
.source "Objects.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static equal(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    if-eq p0, p1, :cond_1

    if-eqz p0, :cond_0

    .line 38
    invoke-virtual {p0, p1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p0

    if-nez p0, :cond_1

    :cond_0
    const/4 p0, 0x0

    return p0

    :cond_1
    const/4 p0, 0x1

    return p0
.end method

.method public static varargs hashCode([Ljava/lang/Object;)I
    .locals 0

    .line 59
    invoke-static {p0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result p0

    return p0
.end method
