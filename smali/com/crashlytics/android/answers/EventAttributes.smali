.class public final Lcom/crashlytics/android/answers/EventAttributes;
.super Ljava/lang/Object;
.source "EventAttributes.java"


# static fields
.field public static final MAX_NUM_ATTRIBUTES:I = 0x14

.field public static final MAX_STRING_LENGTH:I = 0x64


# instance fields
.field final attributes:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 29
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/crashlytics/android/answers/EventAttributes;->attributes:Ljava/util/Map;

    return-void
.end method

.method static validateStringLength(Ljava/lang/String;)V
    .locals 3

    .line 85
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result p0

    const/16 v0, 0x64

    if-gt p0, v0, :cond_0

    return-void

    .line 86
    :cond_0
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    aput-object v0, v1, v2

    const-string v0, "String cannot be longer than %d characters"

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method


# virtual methods
.method public put(Ljava/lang/String;Ljava/lang/Number;)Lcom/crashlytics/android/answers/EventAttributes;
    .locals 0

    if-eqz p1, :cond_1

    if-eqz p2, :cond_0

    .line 67
    invoke-static {p1}, Lcom/crashlytics/android/answers/EventAttributes;->validateStringLength(Ljava/lang/String;)V

    .line 68
    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/answers/EventAttributes;->putAttribute(Ljava/lang/String;Ljava/lang/Object;)V

    return-object p0

    .line 65
    :cond_0
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "value must not be null"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 62
    :cond_1
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "key must not be null"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public put(Ljava/lang/String;Ljava/lang/String;)Lcom/crashlytics/android/answers/EventAttributes;
    .locals 0

    if-eqz p1, :cond_1

    if-eqz p2, :cond_0

    .line 46
    invoke-static {p1}, Lcom/crashlytics/android/answers/EventAttributes;->validateStringLength(Ljava/lang/String;)V

    .line 47
    invoke-static {p2}, Lcom/crashlytics/android/answers/EventAttributes;->validateStringLength(Ljava/lang/String;)V

    .line 48
    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/answers/EventAttributes;->putAttribute(Ljava/lang/String;Ljava/lang/Object;)V

    return-object p0

    .line 44
    :cond_0
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "value must not be null"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 41
    :cond_1
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "key must not be null"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method putAttribute(Ljava/lang/String;Ljava/lang/Object;)V
    .locals 2

    .line 77
    iget-object v0, p0, Lcom/crashlytics/android/answers/EventAttributes;->attributes:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->size()I

    move-result v0

    const/16 v1, 0x14

    if-lt v0, v1, :cond_1

    iget-object v0, p0, Lcom/crashlytics/android/answers/EventAttributes;->attributes:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 78
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    const/4 p2, 0x1

    new-array p2, p2, [Ljava/lang/Object;

    const/4 v0, 0x0

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    aput-object v1, p2, v0

    const-string v0, "Event cannot have more than %d attributes"

    invoke-static {v0, p2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 81
    :cond_1
    :goto_0
    iget-object v0, p0, Lcom/crashlytics/android/answers/EventAttributes;->attributes:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method
